namespace Cafestore.Domain.Models.OrderModels;

public class UpdateOrderDto
{
    public string? ClientName { get; set; }

    public OrderStatus? Status { get; set; }

    public PaymentType? PaymentType { get; set; }

    public IList<AssortmentItemIdDto> OrderItems { get; set; } = [];
}
