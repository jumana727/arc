# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy CA certificates from the host
COPY ./company.crt /usr/local/share/ca-certificates/

RUN update-ca-certificates

WORKDIR /src

# Copy the project files and restore dependencies
COPY ["Arc.Gateway/Arc.Gateway.csproj", "Arc.Gateway/"]
RUN dotnet restore "Arc.Gateway/Arc.Gateway.csproj"

# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/Arc.Gateway"
RUN dotnet build "Arc.Gateway.csproj" -c Release -o /app/build

# Publish the application to a folder
RUN dotnet publish "Arc.Gateway.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the Gateway runs on
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "Arc.Gateway.dll"]

# # Use the official .NET SDK image to build the project
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src

# # Copy the project files and restore dependencies
# COPY ["Arc.Gateway/Arc.Gateway.csproj", "Arc.Gateway/"]
# RUN dotnet restore "Arc.Gateway\Arc.Gateway.csproj"

# # Copy the remaining files and build the application
# COPY . .
# WORKDIR "/ARCSERVER//Arc.Gateway"
# RUN dotnet build "Arc.Gateway.csproj" -c Release -o /app/build

# # Publish the application to a folder
# RUN dotnet publish "Arc.Gateway.csproj" -c Release -o /app/publish

# # Use the official ASP.NET Core runtime image for the runtime environment
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# WORKDIR /app
# COPY --from=build /app/publish .

# # Expose the port the Gateway runs on
# EXPOSE 80

# # Run the application
# ENTRYPOINT ["dotnet", "Arc.Gateway.dll"]
