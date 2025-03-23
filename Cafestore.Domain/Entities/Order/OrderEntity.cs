namespace Cafestore.Domain.Entities.Order;

public class OrderEntity : BaseEntity
{
    public required string ClientName { get; set; }

    public required PaymentType PaymentType { get; set; }

    public required OrderStatus OrderStatus { get; set; }

    public IList<AssortmentItemEntity> OrderItems { get; set; } = [];
}
