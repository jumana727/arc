# Deploying on linux host

## Supervisor : Process Control

#### Accessing supervisorctl


#### Accessing WebUI 

- in /etc/supervisor/supervisord.conf add lines
```
[inet_http_server]
port=*:9001
username=your_username
password=your_password
```

## Configure ssl for nginx gateway

[setup-self-signed-certificate](https://www.humankode.com/ssl/create-a-selfsigned-certificate-for-nginx-in-5-minutes/)