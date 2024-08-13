using MediatR;
using Shared;

namespace Order.API.Application.Queries.GetAll;
public record GetAllOrdersQuery : IRequest<Result<List<Shared.Dtos.Order.OrderDto>>>;
