using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IUserDbContext : IDbContextAccessor
    {
        DbSet<Domain.Entities.User> Users { get; }
    }
}
