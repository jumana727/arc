# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy CA certificates from the host
COPY ./company.crt /usr/local/share/ca-certificates/

RUN update-ca-certificates

WORKDIR /src

# Copy the project files and restore dependencies
COPY ["Configuration.API/Configuration.API.csproj", "Configuration.API/"]
COPY ["../Arc.Common/Arc.Common.csproj", "Arc.Common/"]
COPY ["../Configuration.Application/Configuration.Application.csproj", "Configuration.Application/"]
COPY ["../Configuration.Domain/Configuration.Domain.csproj", "Configuration.Domain/"]
COPY ["../Configuration.Infrastructure/Configuration.Infrastructure.csproj", "Configuration.Infrastructure/"]

RUN dotnet restore "Configuration.API/Configuration.API.csproj"

# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/Configuration.API"
RUN dotnet build "Configuration.API.csproj" -c Release -o /app/build

# Publish the application to a folder
RUN dotnet publish "Configuration.API.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the Configuration.API runs on
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "Configuration.API.dll"]
