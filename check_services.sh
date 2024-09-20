# Check services
declare -A services=(
    [arc-webclient]="http://localhost:4200"
    [arc-authentication]="http://localhost:5174"
    [arc-configuration]="http://localhost:5011"
    [arc-authenticationapi]="http://localhost:5013"
    [arc-vpm]="http://localhost:5075"
    [arc-configurationapi]="http://localhost:5012"
    [keycloak]="http://localhost:8080"
)

for service in "${!services[@]}"; do
    echo "Checking $service at ${services[$service]}..."
    response=$(curl --write-out "%{http_code}\n" --silent --output /dev/null "${services[$service]}")
    if [ "$response" -eq 200 ]; then
        echo "$service is running."
    else
        echo "$service is not running (HTTP code: $response)."
    fi
done
