using System.ComponentModel.DataAnnotations;

namespace Cafestore.Domain.Models.OrderModels;

public class CreateOrderDto : OrderDto
{
    public CreateOrderDto(string clientName, PaymentType paymentType)
    {
        ClientName = clientName;
        PaymentType = paymentType;
        Status = OrderStatus.AtWork;
        OrderItems = new List<AssortmentItemDto>();
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
    [Required]
    [MinLength(5)]
    public required virtual string ClientName { get; set; }

    [Required]
    public required OrderStatus Status { get; set; }

    [Required]
    public required PaymentType PaymentType { get; set; }


    public IList<AssortmentItemDto>? OrderItems { get; set; }
}
