using MediatR;
using Shared;

namespace Application.Order.Commands.Create;
public record CreateOrderRequest : IRequest<Result<Guid>>
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }

    public CreateOrderRequest(Guid userId, string? title)
    {
        UserId = userId;
        Title = title;
    }
}

