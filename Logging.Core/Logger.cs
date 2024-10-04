// using System;
// using System.Net.Http;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;

// namespace Logging.Core
// {
//     public static class Logger 
//     {
//         private static readonly HttpClient _httpClient = new HttpClient();

//         public static async Task LogAsync(string serviceName, string logLevel, string message)
//         {
//             var logData = new
//             {
//                 serviceName = serviceName,
//                 logLevel = logLevel,
//                 message = message,
//                 timestamp = DateTime.Now
//             };

//             var jsonContent = JsonSerializer.Serialize(logData);
//             var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
//             var count=0;
            
//             try
//             {
//                var response = await _httpClient.PostAsync("http://loggingService:8080/Logs", content);
//                         if (!response.IsSuccessStatusCode)
//                         {
//                             // Handle failure response
//                             Console.WriteLine($"Failed to log: {response.StatusCode} - {response.ReasonPhrase}");
//                         }

//             }
//             catch (Exception ex)
//             {
//                 // Handle exception
//                 Console.WriteLine($"Exception while logging: {ex.Message}");
//             }
//         }
//     }
// }

using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Logging.Core
{
    public static class Logger
    {
        private static ILogger? _logger;

        public static void Configure(IConfiguration configuration, string environment)
        {
            try
            {
                var elasticUri = configuration["ElasticConfiguration:Uri"];
                Console.WriteLine($"Configuring logger with Elasticsearch URI: {elasticUri}");

                if (string.IsNullOrEmpty(elasticUri))
                {
                    throw new ArgumentException("Elasticsearch URI is not configured.");
                }

                var indexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environment.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM-dd}";
                Console.WriteLine($"Using index format: {indexFormat}");

                _logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = indexFormat
                    })
                    .Enrich.WithProperty("Environment", environment)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                Log.Logger = _logger;
                Console.WriteLine("Logger configured successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configuring logger: {ex}");
            }
        }

        public static void LogMessage(string serviceName, string logLevel, string message)
        {
            try
            {
                if (_logger == null)
                {
                    throw new InvalidOperationException("Logger is not configured. Call Configure method first.");
                }

                var enrichedLogger = _logger
                    .ForContext("ServiceName", serviceName)
                    .ForContext("Timestamp", DateTime.Now);

                switch (logLevel.ToLower())
                {
                    case "information":
                        enrichedLogger.Information(message);
                        break;
                    case "debug":
                        enrichedLogger.Debug(message);
                        break;
                    case "warning":
                        enrichedLogger.Warning(message);
                        break;
                    case "error":
                        enrichedLogger.Error(message);
                        break;
                    default:
                        enrichedLogger.Verbose(message);
                        break;
                }

                Console.WriteLine($"Log message sent: Service={serviceName}, Level={logLevel}, Message={message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging message: {ex}");
            }
        }
    }
}