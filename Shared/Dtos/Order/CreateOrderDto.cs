namespace Shared.Dtos.Order
{
    public sealed record CreateOrderDto(Guid UserId, string Title);
}
