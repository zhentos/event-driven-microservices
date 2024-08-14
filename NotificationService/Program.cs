var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<NotificationBackgroundService>();

var app = builder.Build();

app.Run();