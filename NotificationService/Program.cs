var builder = Host.CreateApplicationBuilder(args);

// Configuration settings (replace with your actual RabbitMQ settings)
var hostName = "localhost"; // or your RabbitMQ server address
var userName = "guest";
var password = "guest";

builder.Services.AddSingleton(sp => new RabbitMqConsumer(hostName, userName, password));
builder.Services.AddHostedService(sp => sp.GetRequiredService<RabbitMqConsumer>());

var app = builder.Build();

app.Run();