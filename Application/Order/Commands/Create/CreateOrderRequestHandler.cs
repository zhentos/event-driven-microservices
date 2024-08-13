using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Order.Commands.Create
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Result<Guid>>
    {
        private readonly IOrderDbContext _orderDbContext;
        private readonly ILogger<CreateOrderRequestHandler> _logger;
        private readonly IMapper _mapper;
        public CreateOrderRequestHandler(IOrderDbContext orderDbContext, IMapper mapper, ILogger<CreateOrderRequestHandler> logger)
        {
            _orderDbContext = orderDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<Guid>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderDbContext.Orders
                    .AddAsync(new Domain.Entities.Order { UserId = request.UserId, Title = request.Title }, cancellationToken);

                var result = await _orderDbContext.SaveChanges(cancellationToken);

                if (result > 0)
                {
                    return Result<Guid>.Ok(order.Entity.Id);
                }

                return Result<Guid>.Error($"Order wasn't created for the user {request.UserId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<Guid>.Error(ex.Message);
            }
        }
    }
}
