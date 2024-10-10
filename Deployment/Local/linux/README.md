# Deploying on linux host

1. Install Supervisor process controller: [supervisor](http://supervisord.org/installing.html#installing-to-a-system-with-internet-access)

2. Install nginx from apt repository

3. Copy microservices.conf into nginx  folder 
    `cp ./nginx/microservices.conf /etc/supervisor/conf.d`

4. Update dotnet executable, logs, directory paths in Supervisor/microservices.conf 

5. Copy microservices.conf into supervisor  folder 
    `cp ./Supervisor/microservices.conf /etc/nginx/conf.d`

6. Restart servcies 
    `sudo systemctl restart supervisor`
    `sudo systemctl restart nginx`

7. Start Supervisor services 
    `supervisorctl reread`
    `supervisorctl update`

Application will be accessible at http://localhost and https://localhost if ssl is configured 

## Supervisor : Process Control

#### Accessing supervisorctl
RUN `supervisorctl`

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