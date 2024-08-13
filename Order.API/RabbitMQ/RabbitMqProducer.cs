using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Order.API.RabbitMQ
{
    public class RabbitMqProducer : IMessageProducer
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMqProducer(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void SendMessage<T>(T message)
        {
            //using var channel = _connection.Connection.CreateModel();

            _channel.QueueDeclare("orders", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "", routingKey: "orders", mandatory: false, basicProperties: null, body: body);
        }
    }
}
