using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Events;
using System.Text;
using System.Text.Json;

public class RabbitMqConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqConsumer(string hostName, string userName, string password)
    {
        var factory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("orders", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

            if (order != null)
            {
                Console.WriteLine($"Received Order: CorrelationId={order.CorrelationId}, OrderId={order.OrderId}, UserId={order.UserId}");
            }
            else
            {
                Console.WriteLine("Received null order.");
            }

            // Acknowledge the message
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: "orders", autoAck: false, consumer: consumer);
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _channel.Close();
        _connection.Close();
        return base.StopAsync(stoppingToken);
    }
}
