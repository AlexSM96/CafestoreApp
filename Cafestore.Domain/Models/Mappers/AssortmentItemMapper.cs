namespace Cafestore.Domain.Models.Mappers;

public static class AssortmentItemMapper
{
    public static AssortmentItemDto ToDto(this AssortmentItemEntity entity) => 
        new AssortmentItemDto(entity.Name);
    
    public static IList<AssortmentItemDto> ToListDto(this IEnumerable<AssortmentItemEntity> entities)
    {
        return entities
            .Select(e => e.ToDto())
            .ToList();
    }
       
    public static AssortmentItemEntity ToEntity(this AssortmentItemDto assortmentItemDto)
    {
        return new AssortmentItemEntity() { Name = assortmentItemDto.Name };
    }

    public static IList<AssortmentItemEntity> ToListEntity(this IEnumerable<AssortmentItemDto> dtos)
    {
        return dtos
            .Select(x => x.ToEntity())
            .ToList();
    }

}
