using Application.User.Queries.GetAll;
using Application.User.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Dtos.User;

namespace User.API.Apis;

public static class UsersApi
{
    public static RouteGroupBuilder MapOrders(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/users");

        api.MapGet("", GetAll);
        api.MapGet("{id:Guid}", GetUser);

        return api;
    }

    public static async Task<Results<Ok<List<UserDto>>, NotFound>> GetAll(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllUsersQuery());

        if (result.IsOk)
        {
            return TypedResults.Ok(result.Data);
        }
        return TypedResults.Ok(new List<UserDto>());
    }

    public static async Task<Results<Ok<UserDto>, NotFound>> GetUser(IMediator mediator, Guid id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id));

        if (result.IsOk)
        {
            return TypedResults.Ok(result.Data);
        }
        return TypedResults.NotFound();
    }
}
