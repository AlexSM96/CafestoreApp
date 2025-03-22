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
            Products = entity.Products?.ToListDto()  
        };
    }

    public static OrderEntity ToEntity(this OrderDto orderDto)
    {
        return new OrderEntity()
        {
            ClientName = orderDto.ClientName,
            OrderStatus = orderDto.Status,
            PaymentType = orderDto.PaymentType,
            Products = orderDto.Products?.ToListEntity()
        };
    }
}
