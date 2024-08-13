using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Dtos.Order;

namespace Application.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        private readonly IOrderDbContext _orderDbContext;
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IOrderDbContext orderDbContext, IMapper mapper, ILogger<GetOrderByIdQueryHandler> logger)
        {
            _orderDbContext = orderDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderDbContext.Orders.FirstOrDefaultAsync(order => order.Id == request.Id)
                                                                             ?? new Domain.Entities.Order();
                return Result<OrderDto>.Ok(_mapper.Map<OrderDto>(order));

            }
            catch (Exception ex)
            {
                return Result<OrderDto>.Error(ex.Message);
            }
        }
    }
}
