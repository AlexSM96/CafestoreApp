namespace Cafestore.Domain.Models.AssortmentModels;

public class CreateAssortmentItem
{
    [Required(ErrorMessage = "Необходимо ввести название")]
    [MinLength(1, ErrorMessage = "Назвнаие должно быть равна или больше 1 символа")]
    public string Name { get; set; }
}
