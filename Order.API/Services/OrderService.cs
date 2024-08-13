using Order.API.RabbitMQ;

namespace Order.API.Services;

public class OrderService : IOrderService
{
    private readonly IMessageProducer _messageProducer;

    public OrderService(IMessageProducer messageProducer)
    {
        _messageProducer = messageProducer;
    }

    //public async Task<Models.Order.Order> Create(OrderDto orderDto)
    //{
    //    _messageProducer.SendMessage(orderDto);
    //    return new Models.Order.Order();
    //}
}
