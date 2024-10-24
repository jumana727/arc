
## Configure containerd to allow insecure registry
sudo nano /etc/containerd/config.toml

[plugins."io.containerd.grpc.v1.cri".registry.mirrors]
  [plugins."io.containerd.grpc.v1.cri".registry.mirrors."matrix.local.registry:5000"]
    endpoint = ["http://matrix.local.registry:5000"]

sudo systemctl restart containerd

kubectl -n kubernetes-dashboard create token admin-user

