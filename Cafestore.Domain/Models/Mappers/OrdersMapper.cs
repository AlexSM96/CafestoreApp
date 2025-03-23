namespace Cafestore.Domain.Models.Mappers;

public static class OrdersMapper
{
    public static OrderDto ToDto(this OrderEntity entity)
    {
        return new OrderDto()
        {
            ClientName = entity.ClientName,
            Status = entity.OrderStatus,
            PaymentType = entity.PaymentType,
            CreatedAt = entity.CreatedAt,
            OrderItems = entity.OrderItems.ToListDto()  
        };
    }

    public static OrderEntity ToEntity(this OrderDto orderDto)
    {
        return new OrderEntity()
        {
            ClientName = orderDto.ClientName!,
            OrderStatus = orderDto.Status!.Value,
            PaymentType = orderDto.PaymentType!.Value,
            OrderItems = orderDto.OrderItems.ToListEntity()
        };
    }

    public static OrderEntity ToEntity(this CreateOrderDto orderDto, IEnumerable<AssortmentItemEntity> assortmentItems)
    {
        return new OrderEntity()
        {
            ClientName = orderDto.ClientName!,
            OrderStatus = OrderStatus.AtWork,
            PaymentType = orderDto.PaymentType,
            OrderItems = assortmentItems.ToList()
        };
    }

    public static UpdateOrderDto ToUpdateDto(this OrderEntity entity) 
    {
        return new UpdateOrderDto()
        {
            ClientName = entity.ClientName,
            Status = entity.OrderStatus,
            PaymentType = entity.PaymentType,
            OrderItems = entity.OrderItems
                .Select(e => new AssortmentItemIdDto() { Id = entity.Id })
                .ToList(), 
        };
    }
}
