using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderDbContext : DbContext, IOrderDbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Order> Orders { get; set; }

    public DbContext Context => this;

    public Task<int> SaveChanges(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Order>()
            .ToTable("Orders")
            .HasKey(o => o.Id);

        modelBuilder.Entity<Domain.Entities.Order>()
            .Property(o => o.UserId)
            .HasColumnType("uuid");
    }

}
