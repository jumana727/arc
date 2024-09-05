using IdentityServer.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure IdentityServer
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Clients.Get())
    .AddInMemoryApiScopes(ApiScopes.Get())
    .AddInMemoryApiResources(ApiResources.Get())
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
