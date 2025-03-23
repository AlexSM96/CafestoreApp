namespace Cafestore.Domain.Services;

public class AssortmentService(ICafestoreDbContext context) : IAssortmentService
{
    private readonly ICafestoreDbContext _context = context;

    public async Task<IEnumerable<AssortmentItemDto>> GetAssortments()
    {
        return await _context.AssortmentItems
            .AsNoTracking()
            .Select(entity => entity.ToDto())
            .ToListAsync();
    }

    public async Task<AssortmentItemDto> AddAssortmentItem(CreateAssortmentItem assortmentItem)
    {
        if (assortmentItem is null || string.IsNullOrWhiteSpace(assortmentItem.Name))
        {
            throw new InvalidOperationException("Название продукта из ассортимента не должно быть пустым");
        }

        var existedEntity = await _context.AssortmentItems.FirstOrDefaultAsync(e => e.Name == assortmentItem.Name);
        if(existedEntity is not null)
        {
           throw new AlreadyExistsException(assortmentItem.Name);
        }

        var entity = new AssortmentItemEntity()
        {
            Name = assortmentItem.Name,
        };

        var addResult = await _context.AssortmentItems.AddAsync(entity);
        await _context.SaveChangesAsync();

        return addResult.Entity.ToDto();
    }

    public async Task DeleteAssortmentItem(long itemId)
    {
        var itemToDelete = await _context.AssortmentItems.FindAsync(itemId);
        if (itemToDelete is null)
        {
            throw new EntityNotFoundException($"Сущность {nameof(AssortmentItemEntity)} с Id {itemId} не существует");
        }

        _context.AssortmentItems.Remove(itemToDelete);
        await _context.SaveChangesAsync();
    }
}
