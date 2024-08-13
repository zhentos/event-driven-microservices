using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserDbContext : DbContext, IUserDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbContext Context => this;
    public Task<int> SaveChanges(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
}
