# Use an official base image
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
RUN apt-get install -y g++ && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /src

COPY GStreamer/src/ .

RUN mkdir lib

RUN g++ -fPIC -shared -o /src/lib/libstreaming.so Streaming_api.cpp Pipeline.cpp -std=c++11 $(pkg-config --cflags --libs gstreamer-1.0)

# Set the default command
# CMD ["sleep", "infinity"]
