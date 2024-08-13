using MediatR;
using Shared;

namespace Application.Order.Commands.Update;

public record UpdateOrderRequest : IRequest<Result<bool>>
{
    public Guid OrderId { get; set; }
    public string? Title { get; set; }

    public UpdateOrderRequest(Guid orderId, string? title)
    {
        OrderId = orderId;
        Title = title;
    }
}
