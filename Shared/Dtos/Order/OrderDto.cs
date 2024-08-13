namespace Shared.Dtos.Order
{
    public sealed record OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
    }
}
