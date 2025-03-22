namespace Cafestore.Domain.Models.Mappers;

public static class AssortmentItemMapper
{
    public static AssortmentItemDto ToDto(this AssortmentItemEntity entity) => 
        new AssortmentItemDto(entity.Id, entity.Name);
    
    public static IList<AssortmentItemDto> ToListDto(this IEnumerable<AssortmentItemEntity> entities) =>
        entities
            .Select(e => e.ToDto())
            .ToList();
     
    public static AssortmentItemEntity ToEntity(this AssortmentItemDto assortmentItemDto) =>
        new AssortmentItemEntity() { Name = assortmentItemDto.Name };
    
    public static IList<AssortmentItemEntity> ToListEntity(this IEnumerable<AssortmentItemDto> dtos) =>
        dtos.Select(x => x.ToEntity())
            .ToList();
}
