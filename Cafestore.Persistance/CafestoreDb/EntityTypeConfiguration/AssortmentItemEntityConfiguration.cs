namespace Cafestore.Persistance.CafestoreDb.EntityTypeConfiguration;

internal class AssortmentItemEntityConfiguration : IEntityTypeConfiguration<AssortmentItemEntity>
{
    public void Configure(EntityTypeBuilder<AssortmentItemEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasIndex(e => e.Id)
            .IsUnique();

        builder
            .HasIndex(e => e.Name)
            .IsUnique();
    }
}
