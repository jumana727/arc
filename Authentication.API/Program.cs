using Microsoft.OpenApi.Models;
using Authentication.Application;

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
                var jwksUrl = "http://keycloak.default.svc.cluster.local:8080/realms/myrealm/protocol/openid-connect/certs";

                options.Authority = authority;
                options.Audience = "*";
                options.RequireHttpsMetadata = false;
                options.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration
                {
                    Issuer = issuer,
                    JwksUri = jwksUrl
                };
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "account",
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateLifetime = true,
                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        // Fetch the public keys from the JWKs endpoint
                        var jwkSet = new Microsoft.IdentityModel.Tokens.JsonWebKeySet(new System.Net.Http.HttpClient().GetStringAsync(jwksUrl).Result);
                        return jwkSet.GetSigningKeys();
                    }
                };
            });
            builder.Services.AddAuthorization();

            ApplicationGlobal.DependencyInject(builder.Services);
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            // if (app.Environment.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            // }

            app.UseRouting();  // Enables routing.
            app.UseAuthorization();

            app.MapControllers();
        }

    }

}