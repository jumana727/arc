# Create swarm cluster

#### Requires atleast 1 master and multiple worker nodes

1. Initialize cluster on master node `docker swarm init --advertise-addr <MANAGER-IP>` It will print a join command 

```
    docker swarm join \
    --token SWMTKN-1-49nj1cmql0jkz5s954yi3oex3nedyz0fb0xx14ie39trti4wxv-8vxv8rssmk743ojnwacrr2e7c \
    192.168.99.100:2377

```

2. Run the same command on all the worker nodes

3. Verify cluster status from master node `docker node ls`

# Deploy application on swarm cluster

1. Create overlay network : `docker network create --driver overlay arc-network`

2. Deploy Helper Services Stack : `docker stack deploy -c docker-stack.yml arc-ap`

2. Deploy Microservices Stack : `docker stack deploy -c docker-stack-microservices.yml arc-ap`


#### Deploy Swarm visulaizer

Run 
```
docker service create \
  --name visualizer \
  --publish 8081:8080 \
  --constraint node.role==manager \
  --mount type=bind,source=/var/run/docker.sock,target=/var/run/docker.sock \
  dockersamples/visualizer:latest
```
Access visualizer from http://localhost:8081