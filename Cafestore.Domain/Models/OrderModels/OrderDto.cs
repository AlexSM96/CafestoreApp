namespace Cafestore.Domain.Models.OrderModels;

public class CreateOrderDto : OrderDto
{
    public CreateOrderDto(string clientName, PaymentType paymentType)
    {
        ClientName = clientName;
        PaymentType = paymentType;
        Status = OrderStatus.AtWork;
        Products = new List<AssortmentItemDto>();
    }

    public override required string ClientName 
    { 
        get => base.ClientName; 
        set 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            base.ClientName = value;
        } 
    }
}

public class OrderDto
{
    public required virtual string ClientName { get; set; }

    public required OrderStatus Status { get; set; }

    public required PaymentType PaymentType { get; set; }

    public IList<AssortmentItemDto>? Products { get; set; }
}
