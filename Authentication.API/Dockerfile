# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy CA certificates from the host
COPY ./company.crt /usr/local/share/ca-certificates/

RUN update-ca-certificates

WORKDIR /src

# Copy the project files and restore dependencies
COPY ["Authentication.API/Authentication.API.csproj", "Authentication.API/"]
COPY ["../Arc.Common/Arc.Common.csproj", "Arc.Common/"]
COPY ["../Authentication.Domain/Authentication.Domain.csproj", "Authentication.Domain/"]
COPY ["../Authentication.Infrastructure/Authentication.Infrastructure.csproj", "Authentication.Infrastructure/"]
COPY ["../Authentication.Application/Authentication.Application.csproj", "Authentication.Application/"]

RUN dotnet restore "Authentication.API/Authentication.API.csproj"

# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/Authentication.API"
RUN dotnet build "Authentication.API.csproj" -c Release -o /app/build

# Publish the application to a folder
RUN dotnet publish "Authentication.API.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the Authentication.API runs on
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "Authentication.API.dll"]
