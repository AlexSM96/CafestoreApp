using Cafestore.Domain.Models.Mappers;

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

    public async Task<AssortmentItemDto> AddAssortmentItem(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        var assortmentItem = new AssortmentItemEntity()
        {
            Name = name
        };

        var addResult = await _context.AssortmentItems.AddAsync(assortmentItem);
        await _context.SaveChangesAsync();

        return addResult.Entity.ToDto();
    }

    public async Task DeleteAssortmentItem(long itemId)
    {
         await _context.AssortmentItems
            .Where(entity => entity.Id == itemId)
            .ExecuteDeleteAsync();
    }
}
