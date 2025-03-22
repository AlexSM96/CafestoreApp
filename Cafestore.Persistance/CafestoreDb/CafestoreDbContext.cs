namespace Cafestore.Persistance.CafestoreDb;

public class CafestoreDbContext(DbContextOptions<CafestoreDbContext> contextOptions) 
    : DbContext(contextOptions)
{

    public DbSet<AssortmentItemEntity> AssormentItems { get; set; }

    public DbSet<OrderEntity> Orders { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
