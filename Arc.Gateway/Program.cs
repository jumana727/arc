using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var keycloakAuthority = builder.Configuration.GetValue<string>("KeycloakAuthority") ?? "http://localhost:8080/realms/myrealm";
var ocelotConfigFile = builder.Configuration.GetValue<string>("OcelotConfigFile") ?? "ocelot.json";

// Add configuration to read Ocelot.json
builder.Configuration.AddJsonFile(ocelotConfigFile, optional: false, reloadOnChange: true);

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
    options.Authority = keycloakAuthority;
    options.Audience = "*";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "account",
        ValidateIssuer = true,
        ValidIssuer = keycloakAuthority,
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
