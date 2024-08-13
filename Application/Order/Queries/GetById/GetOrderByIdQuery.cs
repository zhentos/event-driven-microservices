using MediatR;
using Shared;
using Shared.Dtos.Order;

namespace Application.Order.Queries.GetById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderDto>>;
}
