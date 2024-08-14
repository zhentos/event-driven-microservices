using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class NotificationBackgroundService : BackgroundService
{
    private readonly ILogger<NotificationBackgroundService> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName = "orders";
    private readonly int _maxRetries = 3;
    private readonly int _retryDelay = 5000; // 5 seconds

    public NotificationBackgroundService(ILogger<NotificationBackgroundService> logger)
    {
        _logger = logger;

        var factory = new ConnectionFactory()
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);

            try
            {
                await ProcessNotificationAsync(message);
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing notification: {Message}", message);
                await HandleRetryAsync(ea, ex);
            }
        };

        _channel.BasicConsume(_queueName, autoAck: false, consumer: consumer);

        _logger.LogInformation("Notification background service started.");
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task ProcessNotificationAsync(string message)
    {
        // Process the notification message
        // Example: Send an email notification
        await SendEmailNotificationAsync(message);
    }

    private async Task SendEmailNotificationAsync(string message)
    {
        // Code to send email notification
        await Task.Delay(2000); // Simulating email sending delay
        _logger.LogInformation("Email notification sent: {Message}", message);
    }

    private async Task HandleRetryAsync(BasicDeliverEventArgs ea, Exception ex, int retryCount = 0)
    {
        if (retryCount < _maxRetries)
        {
            _logger.LogWarning(ex, "Retrying notification processing. Retry count: {RetryCount}", retryCount + 1);
            await Task.Delay(_retryDelay);
            await ProcessNotificationAsync(System.Text.Encoding.UTF8.GetString(ea.Body.ToArray()));
        }
        else
        {
            _logger.LogError(ex, "Maximum retries reached for notification. Moving to dead-letter queue.");
            _channel.BasicNack(ea.DeliveryTag, false, false);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Notification background service stopping.");
        await base.StopAsync(cancellationToken);
        _channel.Close();
        _connection.Close();
    }
}
