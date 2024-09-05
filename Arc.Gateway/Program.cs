using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add configuration to read Ocelot.json
builder.Configuration.AddJsonFile("ocelot.Docker.json", optional: false, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configure authentication
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "http://keycloak:8080/realms/myrealm";
    options.Audience = "*";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "account",
        ValidateIssuer = true,
        ValidIssuer = "http://keycloak:8080/realms/myrealm",
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization();

// Add Ocelot services
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseCors("AllowAll");

// Use authentication and Ocelot middleware
app.UseAuthentication();
app.UseOcelot().Wait();

app.Run();
