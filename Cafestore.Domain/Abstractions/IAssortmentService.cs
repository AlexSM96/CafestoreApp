namespace Cafestore.Domain.Abstractions;

public interface IAssortmentService
{
    public Task<IEnumerable<AssortmentItemDto>> GetAssortments();

    public Task<AssortmentItemDto> AddAssortmentItem(CreateAssortmentItem assortmentItem);

    public Task DeleteAssortmentItem(long itemId);
}
