using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IOrderDbContext : IDbContextAccessor
    {
        DbSet<Domain.Entities.Order> Orders { get; }
    }
}
