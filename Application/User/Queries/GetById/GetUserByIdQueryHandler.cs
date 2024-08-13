using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Dtos.User;

namespace Application.User.Queries.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserDbContext _userDbContext;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserDbContext userDbContext, IMapper mapper, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userDbContext = userDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userDbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id)
                                                                             ?? new Domain.Entities.User();
                return Result<UserDto>.Ok(_mapper.Map<UserDto>(user));

            }
            catch (Exception ex)
            {
                return Result<UserDto>.Error(ex.Message);
            }
        }
    }
}
