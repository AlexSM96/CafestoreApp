namespace Cafestore.Domain.Abstractions;

public interface ICafestoreDbContext
{
    public DbSet<AssortmentItemEntity> AssortmentItems { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
