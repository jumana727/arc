using PushNotifications;
using PushNotifications.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
if (string.IsNullOrEmpty(connectionString))
    throw new Exception("Connection string is empty.");
builder.Services.AddAppDbContext(connectionString);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<UserAndTokensRepository>();
builder.Services.AddScoped<UserAndTokensService>();

builder.Services.AddSingleton<FirebaseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/recordToken", (string userId, string fcmToken, UserAndTokensService userAndTokensService)
    => userAndTokensService.AddRecord(fcmToken, userId))
.WithOpenApi();

app.MapGet("/sendNotifications", async (string userId, string? title, string? body,
    UserAndTokensService userAndTokensService, FirebaseService firebaseService,
    ILogger<Program> logger) =>
    {
        var userAndTokens = await userAndTokensService.GetFcmTokens(userId);
        if (userAndTokens.Count <= 0)
        {
            logger.LogDebug("User {userId} has not registered for push notifications.", userId);
            return;
        }

        var notificationTasksQuery = userAndTokens.Select(userAndToken => 
            firebaseService.SendNotificationAsync(userAndToken.FcmToken, title, body));
        var notificationTasks = notificationTasksQuery.ToList();

        await Task.WhenAll(notificationTasks);
    })
    .WithOpenApi();

app.Run();
