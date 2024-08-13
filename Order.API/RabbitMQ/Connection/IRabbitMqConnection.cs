using RabbitMQ.Client;

namespace Order.API.RabbitMQ.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}
