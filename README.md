# Arc ELK Logging
## This project implements centralized logging using the ELK stack (Elasticsearch, Logstash, and Kibana) across four components: Frontend (ArcClient), Backend, Keycloak, and C++ (VPM/GStreamer).

### 1. Frontend (ArcClient)
   Folder Path: `ArcClient/src/app/shared/services/LoggingService`

    In the frontend, logging is implemented via a service called LoggingService. This service sends logs to Elasticsearch by making API calls. The log data is structured based on a predefined format.

    Logging Steps:
    Use the LoggingService wherever logging is needed in the frontend.
    Fill in the predefined log structure with relevant information.
    Call the appropriate API endpoint to send the log to Elasticsearch.
    The LoggingService ensures all logs are sent in the required format to Elasticsearch.

### 2. Backend
  Folder Path: `Logging.Core`

    In the backend, we created a reusable library called Logging.Core for logging purposes. This library can be used across multiple microservices.

    Implementation:
    Folder Path: Authentication.Api, Configuration.Api
    Steps:
    Add a reference to the Logging.Core library in the microservice.
    In Program.cs, call the Configure method from Logging.Core to set up logging.
    Whenever logging is required, use the LogMessage method from Logging.Core, passing all necessary parameters to log the message to Elasticsearch.


### 3. Keycloak
    In Keycloak, logging is configured to write all logs to the host device (e.g., macOS) in a dedicated Keycloak log folder.

    Steps:
    Enable logging in the Keycloak configuration and direct the logs to the host machine.
    Use Logstash to forward the Keycloak logs to Elasticsearch.
    The Logstash configuration is defined in a file named logstash.conf.
    Logstash Configuration Example (logstash.conf):

    This setup collects logs from Keycloak and forwards them to Elasticsearch for analysis in Kibana.

### 4. C++ (VPM/GStreamer)
   Folder Path: `VPM/GStreammer`

    For logging in the C++ project, we use spdlog, which is configured to send logs to Elasticsearch via a custom sink.

    Steps:
    Implement spdlog in the C++ project to handle logging.
    Configure spdlog to use a custom sink that forwards logs directly to Elasticsearch.
    The logging setup is already implemented in the VPM/GStreamer folder.
    This documentation provides an overview of the logging setup in the Arc project. Each component logs structured data to Elasticsearch, enabling centralized log management and analysis through Kibana.