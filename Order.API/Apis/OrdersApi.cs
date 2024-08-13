using Application.Order.Commands.Create;
using Application.Order.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.API.Application.Queries.GetAll;
using Order.API.RabbitMQ;
using Shared.Dtos.Order;
using Shared.Events;
namespace Order.API.Apis;

public static class OrdersApi
{
    public static RouteGroupBuilder MapOrders(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/orders");

        api.MapGet("", GetAll);
        api.MapGet("{id:Guid}", GetOrder);
        api.MapPost("", CreateOrder);

        return api;
    }

    public static async Task<Results<Ok<List<OrderDto>>, NotFound>> GetAll(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllOrdersQuery());

        if (result.IsOk)
        {
            return TypedResults.Ok(result.Data);
        }
        return TypedResults.Ok(new List<OrderDto>());
    }

    public static async Task<Results<Ok<OrderDto>, NotFound>> GetOrder(IMediator mediator, Guid id)
    {
        var result = await mediator.Send(new GetOrderByIdQuery(id));

        if (result.IsOk)
        {
            return TypedResults.Ok(result.Data);
        }
        return TypedResults.NotFound();
    }

    public static async Task<Results<Ok<Guid>, BadRequest<string>>> CreateOrder(IMediator mediator, IMessageProducer messageProducer, CreateOrderDto dto)
    {
        //todo add validation 
        var result = await mediator.Send(new CreateOrderRequest(dto.UserId, dto.Title));

        if (result.IsOk)
        {
            messageProducer.SendMessage(
                new OrderCreatedEvent
                {
                    CorrelationId = Guid.NewGuid(),
                    OrderId = result.Data,
                    UserId = dto.UserId
                }
                );

            return TypedResults.Ok(result.Data);
        }

        return TypedResults.BadRequest(result.Message);
    }
}
