namespace Cafestore.Domain.Models.OrderModels;

public class CreateOrderDto
{
    private string _clinetName;

    [Required(ErrorMessage = "Необходимо внести имя клиента")]
    [MinLength(1, ErrorMessage = "Минимальная длина 1 символ")]
    public string ClientName 
    { 
        get => _clinetName; 
        set 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _clinetName = value;
        } 
    }

    [Required(ErrorMessage = "Необходимо указать способ оплаты")]
    public PaymentType PaymentType { get; set; }

    [Required(ErrorMessage = "Необходимо выбрать товар для заказа из ассортимента")]
    public IList<AssortmentItemIdDto>? OrderItems { get; set; }
}
