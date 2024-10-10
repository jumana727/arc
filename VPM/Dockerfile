# .NET build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
WORKDIR /src
COPY ./company.crt /usr/local/share/ca-certificates/
RUN update-ca-certificates

COPY ["VPM/VPM.csproj", "VPM/"]
COPY ["../Arc.Common/Arc.Common.csproj", "Arc.Common/"]
COPY ["../VPM.Application/VPM.Application.csproj", "VPM.Application/"]
COPY ["../VPM.Domain/VPM.Domain.csproj", "VPM.Domain/"]
COPY ["../VPM.Infrastructure/VPM.Infrastructure.csproj", "VPM.Infrastructure/"]
RUN dotnet restore "VPM/VPM.csproj"

COPY . .
WORKDIR "/src/VPM"
RUN dotnet publish "VPM.csproj" -c Release -o /app/publish

# GStreamer build stage
FROM ubuntu:20.04 AS gstreamer-build
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /src

COPY ./company.crt /usr/local/share/ca-certificates/

RUN apt-get update && apt-get install -y ca-certificates

RUN update-ca-certificates

RUN apt-get update && apt-get install -y \
    libgstreamer1.0-dev \
    libgstreamer-plugins-base1.0-dev \
    libgstreamer-plugins-bad1.0-dev \
    g++ \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*



COPY VPM/GStreamer/src/ .

RUN g++ -fPIC -shared -o lib/libstreaming.so Streaming_api.cpp Pipeline.cpp -std=c++11 $(pkg-config --cflags --libs gstreamer-1.0)

# Final stage
FROM ubuntu:20.04 AS final
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /src

COPY ./company.crt /usr/local/share/ca-certificates/

RUN apt-get update && apt-get install -y ca-certificates

RUN update-ca-certificates

# Install runtime dependencies
RUN apt-get update && apt-get install -y \
    libgstreamer1.0-0 \
    gstreamer1.0-plugins-base \
    gstreamer1.0-plugins-good \
    gstreamer1.0-plugins-bad \
    gstreamer1.0-plugins-ugly \
    gstreamer1.0-rtsp \
    ca-certificates \
    wget \
    && wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y aspnetcore-runtime-8.0 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

COPY --from=dotnet-build /app/publish .
COPY --from=gstreamer-build /src/lib/libstreaming.so /src/lib/

ENV LD_LIBRARY_PATH=/usr/lib/x86_64-linux-gnu/gstreamer-1.0
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "VPM.dll"]