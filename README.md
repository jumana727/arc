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

## Run with Consul

1. Install consul server and client using helm
    - helm repo add hashicorp https://helm.releases.hashicorp.com
    - helm install --values Helm/values.yaml consul hashicorp/consul --create-namespace --namespace consul --version "1.2.0"
2. To view consul UI, forward its port 
    - kubectl port-forward svc/consul-ui --namespace consul 8501:443
3. Deploy Services in minikube 
    - kubectl apply -f Deployment/Services/authentication-api.yml
    - kubectl apply -f Deployment/Services/configuration-api.yml
    - kubectl apply -f Deployment/WebUI
    - kubectl apply -f Deployment/Keycloak
    - kubectl apply -f Deployment/Database
4. Create a Consul Gateway for entrypoint
    - kubectl apply -f Consul-configs/gateway.yml
5. Define HttpRoutes and ServiceIntentions to enable communication between services
    - kubectl apply -f Consul-configs/routes.yml
6. To enable sercvice splitting between 2 versions of the angular frontent 
    1. Apply a Service Resolver to help consul resolve version of the service
        - kubectl apply -f Consul-configs/serviceresolver.yml
    2. Apply service splitter to split traffic 50-50 between v1 and v2 of the frontend service
        - kubectl apply -f Consul-configs/service-splitter
7. To run Prometheus and Grafana for observability and logs viewing
    - sh Consul-config/install-observability-suite.sh
8. Access grafana by port forwarding grafana's service port
    - kubectl port-forward svc/grafana 3000:3000    
9. Apply circuit breaking to frontent for applying settings for requests sent towards the destination (upstream) service
    - kubectl apply -f Consul-configs/circuit-breaking.yml                                                    