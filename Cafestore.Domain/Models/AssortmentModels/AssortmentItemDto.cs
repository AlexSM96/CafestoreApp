namespace Cafestore.Domain.Models.AssortmentModels;

public class AssortmentItemDto : CreateAssortmentItem
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }
}
