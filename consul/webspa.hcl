service {
    name = "angular-frontend" 
    id = "angular-frontend"  # Unique ID for this service instance
    address = "127.0.0.1"
    port = 4200
    
    tags = ["web", "frontend"]
    
    check {
      name = "Angular Frontend Health Check"
      http = "http://127.0.0.1:4200"
      interval = "10s"
      timeout = "5s"
    }
  
    meta = {
      version = "1.0"
      environment = "development"
    }
  }