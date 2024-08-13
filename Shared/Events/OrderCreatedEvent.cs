﻿namespace Shared.Events
{
    public record OrderCreatedEvent
    {
        public Guid CorrelationId { get; init; }
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
    }
}
