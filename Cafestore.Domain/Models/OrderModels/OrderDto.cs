namespace Cafestore.Domain.Models.OrderModels;

public class OrderDto
{
    public string? ClientName { get; set; }

    public OrderStatus? Status { get; set; }

    public PaymentType? PaymentType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public IList<AssortmentItemDto> OrderItems { get; set; } = [];
}
