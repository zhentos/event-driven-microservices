using MediatR;
using Shared;

namespace Application.Order.Commands.Delete;
public record DeleteOrderRequest : IRequest<Result<bool>>
{
    public Guid OrderId { get; set; }

    public DeleteOrderRequest(Guid orderId)
    {
        OrderId = orderId;
    }
}
