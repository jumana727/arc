### Now we need to find the token we can use to log in. Execute the following command:
kubectl -n kubernetes-dashboard create token admin-user

kubectl get secret admin-user -n kubernetes-dashboard -o jsonpath={".data.token"} | base64 -d