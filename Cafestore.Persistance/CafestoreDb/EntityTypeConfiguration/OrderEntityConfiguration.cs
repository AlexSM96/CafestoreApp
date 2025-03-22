
namespace Cafestore.Persistance.CafestoreDb.EntityTypeConfiguration;

internal class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasIndex(e => e.Id)
            .IsUnique();

        builder
            .HasMany(e => e.Products);

        builder
            .Property(e => e.OrderStatus)
            .IsRequired();

        builder
            .Property(e => e.PaymentType)
            .IsRequired();
    }
}
