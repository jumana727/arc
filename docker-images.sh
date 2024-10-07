#!/bin/bash

# Function to compress Docker images
compress_images() {
    echo "Compressing Docker images..."
    docker save -o arc.tar \
        phadkesharanmatrixcomsec/arc-vpm:hls-1.0 \
        phadkesharanmatrixcomsec/arc-gateway:1.0 \
        phadkesharanmatrixcomsec/arc-configuration:hls-1.0 \
        phadkesharanmatrixcomsec/arc-webspa:hls-1.0 \
        phadkesharanmatrixcomsec/arc-authentication:hls-1.0 \
        quay.io/keycloak/keycloak:latest \
        bluenviron/mediamtx:latest \
        nginx:latest \
        postgres:latest 


    gzip arc.tar
    echo "Compression complete: arc.tar.gz"
}

# Function to extract Docker images from archive
extract_images() {
    echo "Extracting Docker images..."
    if [ -f arc.tar.gz ]; then
        gunzip arc.tar.gz
        docker load -i arc.tar
        echo "Extraction complete."
    else
        echo "Error: arc.tar.gz not found!"
    fi
}

# Display menu options
echo "Choose an option:"
echo "1. Compress Docker images"
echo "2. Extract Docker images"
read -p "Enter your choice (1 or 2): " choice

case $choice in
    1)
        compress_images
        ;;
    2)
        extract_images
        ;;
    *)
        echo "Invalid choice! Exiting..."
        ;;
esac
