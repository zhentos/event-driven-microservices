using FluentValidation;
using Shared.Dtos.Order;

namespace Order.API.Application.Validators
{
    public class OrderValidator : AbstractValidator<CreateOrderDto>
    {
        public OrderValidator()
        {
            RuleFor(o => o.Title)
                .MaximumLength(40);

            RuleFor(o => o.UserId)
                .NotEmpty();
        }
    }
}
