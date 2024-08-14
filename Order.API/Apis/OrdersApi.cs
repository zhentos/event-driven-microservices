using Application.Order.Commands.Create;
using Application.Order.Queries.GetById;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

    public static async Task<Results<Ok<Guid>, BadRequest<object>>> CreateOrder(IMediator mediator,
        IMessageProducer messageProducer,
        [FromServices] IValidator<CreateOrderDto> orderValidator,
        CreateOrderDto dto)
    {
        var validationResult = await orderValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors as object);
        }

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

        return TypedResults.BadRequest(result.Message as object);
    }
}
