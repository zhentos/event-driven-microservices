using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Dtos.Order;

namespace Order.API.Application.Queries.GetAll
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<List<OrderDto>>>
    {
        private readonly IOrderDbContext _orderDbContext;
        private readonly ILogger<GetAllOrdersQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderDbContext orderDbContext, IMapper mapper, ILogger<GetAllOrdersQueryHandler> logger)
        {
            _orderDbContext = orderDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _orderDbContext.Orders.ProjectTo<OrderDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return Result<List<OrderDto>>.Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<OrderDto>>.Error(ex.Message);
            }
        }
    }
}
