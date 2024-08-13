using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Dtos.User;

namespace Application.User.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        private readonly IUserDbContext _userDbContext;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUserDbContext userDbContext, IMapper mapper, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userDbContext = userDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _userDbContext.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return Result<List<UserDto>>.Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserDto>>.Error(ex.Message);
            }
        }
    }
}
