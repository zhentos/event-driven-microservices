using FluentValidation;

namespace Application.Order.Commands.Create
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(o => o.UserId)
            .NotEmpty();

            RuleFor(o => o.Title)
                .MaximumLength(40);
        }
    }
}
