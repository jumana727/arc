using Microsoft.OpenApi.Models;
using Authentication.Application;
using Logging.Core;
using Prometheus;

namespace Authentication.API
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            ConfigureMiddleware(app);
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            Logger.Configure(builder.Configuration, builder.Environment.EnvironmentName);
            Logger.LogMessage("Authapi", "Information", "Logger configured In Auth MicroService");
            // Add controllers to the dependency injection (DI) container.
            builder.Services.AddControllers();

            builder.Services.AddHttpClient();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            if (true)
            {
                // Provides automatic generation of API documentation, making it easier to understand and use the API.
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    // c.SchemaFilter<CustomSchemaFilters>();
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                    Enter 'Bearer' [space] and then your token in the text input below.
                                    \r\n\r\nExample: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });
                });
            }

            builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                // Retrieve configuration from appsettings
                var authenticationConfig = builder.Configuration.GetSection("Authentication");
                var authority = authenticationConfig.GetValue<string>("Authority");
                var issuer = authenticationConfig.GetValue<string>("Issuer");

                options.Authority = authority;
                options.Audience = "*";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "account",
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateLifetime = true
                };
            });
            builder.Services.AddAuthorization();

            ApplicationGlobal.DependencyInject(builder.Services);
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseCors("AllowAll");
            // if (app.Environment.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            // }

            app.UseRouting();  // Enables routing.
            app.UseAuthorization();
            app.UseHttpMetrics(); // To measure HTTP request metrics

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics(); // Exposes /metrics endpoint for Prometheus
                endpoints.MapControllers();
            });
            app.MapControllers();
        }

    }

}