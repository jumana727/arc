kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.0-beta.0/deploy/static/provider/baremetal/deploy.yaml


kubectl port-forward -n ingress-nginx svc/ingress-ngin
x-controller --address 0.0.0.0 4200:80