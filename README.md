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

## Run with Istio

1. Download istio
    - `curl -L https://istio.io/downloadIstio | sh -`
    - `cd istio-1.23.2`
    - `export PATH=$PWD/bin:$PATH`
2. Install Istio
    - `istioctl install -f samples/bookinfo/demo-profile-no-gateways.yaml -y`
3. Add a namespace label to instruct Istio to automatically inject Envoy sidecar proxies when you deploy your application later
    - `kubectl label namespace default istio-injection=enabled`
4. Install the Gateway API CRDs, if they are not already present:
    - `kubectl get crd gateways.gateway.networking.k8s.io &> /dev/null || \
        { kubectl kustomize "github.com/kubernetes-sigs/gateway-api/config/crd?ref=v1.1.0" | kubectl apply -f -; }`
5. Deploy all the application services
    - `kubectl apply -f Deployment/Services/authentication-api.yml`
    - `kubectl apply -f Deployment/Services/configuration-api.yml`
    - `kubectl apply -f Deployment/WebUI`
    - `kubectl apply -f Deployment/Keycloak`
    - `kubectl apply -f Deployment/Database`
6. Create an Istio Gateway for entrypoint
    - `kubectl apply -f Deployment/Gateway/gateway.yml`
    1. Find the gateway's loadbalancer ip
        - `LB_IP=$(kubectl get service example-gateway-istio -o jsonpath='{.status.loadBalancer.ingress[0].ip}')`
    2. Add a DNS entry 
        - `sudo nano /etc/hosts`
        - `<LB_IP> matrix.keycloak`
        - now you can access the application at http://matrix.keycloak/
7. Enable mtls for secure communication within cluster services
    - `kubectl apply -f Deployment/Istio-policies/enable-mtls.yml`
8. To enable load balancing for configuration-api service
    1. Scale configuration-api service to 2 instances
        - `kubectl scale --replicas=2 deployment configuration-api-deployment` 
    2. Apply load balancing
        - `kubectl apply -f Deployment/Istio-policies/load-balancing.yml`
9. Apply circuit breaking to configuration-api service
    - `kubectl apply -f Deployment/Istio-policies/circuit-breaking.yml`
10. Apply http policy to configuration service to only enable GET method for the service
    - `kubectl apply -f Deployment/Istio-policies/http-policy.yml`    
11. Apply monitoring addons 
    - `kubectl apply -f addons/`  
    - Access the Kiali dashboard
        - `istioctl dashboard kiali`                                          