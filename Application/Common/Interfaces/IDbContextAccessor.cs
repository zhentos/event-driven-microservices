using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDbContextAccessor
    {
        public DbContext Context { get; }
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
