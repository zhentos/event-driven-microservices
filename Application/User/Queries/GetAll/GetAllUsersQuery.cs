using MediatR;
using Shared;

namespace Application.User.Queries.GetAll;
public record GetAllUsersQuery : IRequest<Result<List<Shared.Dtos.User.UserDto>>>;
