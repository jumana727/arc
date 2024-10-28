# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy CA certificates from the host
COPY ./company.crt /usr/local/share/ca-certificates/

RUN update-ca-certificates

WORKDIR /src

# Copy the project files and restore dependencies
COPY ["VPM/VPM.csproj", "VPM/"]
COPY ["../Arc.Common/Arc.Common.csproj", "Arc.Common/"]
COPY ["../VPM.Application/VPM.Application.csproj", "VPM.Application/"]
COPY ["../VPM.Domain/VPM.Domain.csproj", "VPM.Domain/"]
COPY ["../VPM.Infrastructure/VPM.Infrastructure.csproj", "VPM.Infrastructure/"]

RUN dotnet restore "VPM/VPM.csproj"

# Copy the remaining files and build the application
COPY . .
WORKDIR "/src/VPM"
RUN dotnet build "VPM.csproj" -c Release -o /app/build

# Publish the application to a folder
RUN dotnet publish "VPM.csproj" -c Release -o /app/publish

# Use an official base image to build GStreamer project
FROM ubuntu:20.04 AS gstreamer-build

ENV DEBIAN_FRONTEND=noninteractive

# Install GStreamer and dependencies
RUN apt-get update && apt-get install -y libgstreamer1.0-dev \
    libgstreamer-plugins-base1.0-dev \
    libgstreamer-plugins-bad1.0-dev \
    gstreamer1.0-plugins-base \
    gstreamer1.0-plugins-good \
    gstreamer1.0-plugins-bad \
    gstreamer1.0-plugins-ugly \
    gstreamer1.0-rtsp

# Install g++ and clean up to reduce image size
RUN apt-get install -y g++ \
    ca-certificates \
    wget && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Copy CA certificates from the host
COPY ./company.crt /usr/local/share/ca-certificates/

RUN update-ca-certificates

WORKDIR /src

COPY VPM/GStreamer/src/ .

RUN g++ -fPIC -shared -o /src/lib/libstreaming.so Streaming_api.cpp Pipeline.cpp -std=c++11 $(pkg-config --cflags --libs gstreamer-1.0)

# Install .NET runtime (ASP.NET Core 8.0)
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y aspnetcore-runtime-8.0 && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .

ENV LD_LIBRARY_PATH=/usr/lib/x86_64-linux-gnu/gstreamer-1.0
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "VPM.dll"]

# # Use the official ASP.NET Core runtime image for the runtime environment
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# WORKDIR /app
# COPY --from=build /app/publish .

# # Copy the GStreamer libraries and libstreaming.so from the gstrea\mer-build stage
# COPY --from=gstreamer-build /usr/lib/x86_64-linux-gnu/ /usr/lib/x86_64-linux-gnu/
# # COPY --from=gstreamer-build /usr/lib/x86_64-linux-gnu/libgstreamer-1.0.so.0 /usr/lib/x86_64-linux-gnu/
# # COPY --from=gstreamer-build /usr/lib/x86_64-linux-gnu/libgobject-2.0.so.0 /usr/lib/x86_64-linux-gnu/

# COPY --from=gstreamer-build /src/lib/libstreaming.so /app/lib/libstreaming.so

# ENV LD_LIBRARY_PATH=/usr/lib/x86_64-linux-gnu/gstreamer-1.0

# # Expose the port the VPM runs on
# EXPOSE 8080

# # Run the application
# ENTRYPOINT ["dotnet", "VPM.dll"]
