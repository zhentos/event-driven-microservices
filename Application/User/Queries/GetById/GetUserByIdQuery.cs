using MediatR;
using Shared;
using Shared.Dtos.User;

namespace Application.User.Queries.GetById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
}
