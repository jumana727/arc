# Arc

## Run using Docker Compose

##### You can comment out vpm service in docker-compose-microservices if Video Management Service is not required (gstreamer)


1. Change Entrypoint URL in ArcClient/src/app/core/config/endpoints.ts file
    - Change BASE_URL to http://localhost:5039/api

2. Update ASPNETCORE_ENVIRONMENT in docker-compose-microservices.yml and Build required docker images 
    - `ASPNETCORE_ENVIRONMENT=Docker`
    - `docker compose -f docker-compose-microservices.yml build`

3. Run all the microservices and helper services

    - `docker compose -f docker-compose.yml -f docker-compose-microservices.yml up`

4. All the microservices are accessible at forwarded ports

    - Apply migrations by running `dotnet ef database update` in Configuration.Infrastructure project
    - Access keycloak admin console at localhost:8080 and Add realm and users


## Run locally

1. Change Entrypoint URL in ArcClient/src/app/core/config/endpoints.ts file
    - Change BASE_URL to http://localhost:5039/api

2. Run all the Microservices 
    - Dotnet WebAPIs : dotnet run --project <project-name>
    - Angular SPA: npm run

## Run with Kong Gateway

### Prerequisites 
- Minikube Cluster
- kubectl
### Steps to install and run
1. Install the Gateway API CRDs before installing Kong Ingress Controller.
    - `kubectl apply -f https://github.com/kubernetes-sigs/gateway-api/releases/download/v1.1.0/standard-install.yaml`
2. Install Kong Ingress Controller and Kong Gateway with Helm
    - `helm repo add kong https://charts.konghq.com`
    - `helm repo update`
    - `helm install kong kong/ingress -n kong --create-namespace `
3. Apply all the services
    - `kubectl apply -f Deployment/Services`
    - `kubectl apply -f Deployment/WebUI`
    - `kubectl apply -f Deployment/Keycloak`
    - `kubectl apply -f Deployment/Database`
4. Create a Gateway and GatewayClass instance to use 
    - `kubectl apply -f KongGateway/gateway.yml`
    1. Find the gateway's loadbalancer ip
        - `LB_IP=$(kubectl get service kong-gateway-proxy -n kong -o jsonpath='{.status.loadBalancer.ingress[0].ip}')`
    2. Add a DNS entry 
        - `sudo nano /etc/hosts`
        - `<LB_IP> matrix.keycloak`
    3. Create routing configuration to proxy requests to server
        - `kubectl apply -f KongGateway/Routes/`
        - now you can access the application at https://matrixcomsec.com/
6. Apply Rate Limiting plugin
    - `kubectl apply -f KongGateway/Plugins/rate-limit.yml`
    - to apply this plugin to a particular service, add annotations to the k8s service of the deployment.
        ```yaml
        apiVersion: v1
        kind: Service
        metadata:
        name: configuration-api
        labels:
            app: configuration-api
        annotations: 
            konghq.com/plugins: "rate-limit-5-min"
        spec:
        selector:
            app: configuration-api
        ports:
            - protocol: TCP
            port: 8080
            targetPort: 8080
        type: ClusterIP
        ```
7. Apply Response caching plugin
    - `kubectl apply -f KongGateway/Plugins/response-caching.yml`
8. Apply Response ip restriction plugin
    - `kubectl apply -f KongGateway/Plugins/ip-restriction.yml`
9. Apply Response load balancing plugin
    - `kubectl apply -f KongGateway/Plugins/loadBalanceConsistentHash.yml`
10. Apply monitoring plugin and install Prometheus & Grafana using Helm
    - kubectl create namespace monitoring
    - `helm repo add prometheus-community https://prometheus-community.github.io/helm-charts`
    - `helm install promstack prometheus-community/kube-prometheus-stack --namespace monitoring --version 52.1.0 -f KongGateway/values-monitoring.yml`
    - `helm upgrade kong kong/ingress -n kong --set gateway.serviceMonitor.enabled=true --set gateway.serviceMonitor.labels.release=promstack`
    - `kubectl apply -f KongGateway/Plugins/monitoring`
11. Access Grafana Dashboard
    - `kubectl -n monitoring port-forward services/promstack-grafana 3000:80`
    - You can access Grafana in your browser at `localhost:3000`                                                  