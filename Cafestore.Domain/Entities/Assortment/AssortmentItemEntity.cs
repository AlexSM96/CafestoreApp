namespace Cafestore.Domain.Entities.Assortment;

public class AssortmentItemEntity : BaseEntity
{
    public required string Name { get; set; }

    public IList<OrderEntity>? Orders { get; set; }
}
