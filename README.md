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