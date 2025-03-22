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
            OrderItems = entity.OrderItems?.ToListDto()  
        };
    }

    public static OrderEntity ToEntity(this OrderDto orderDto)
    {
        return new OrderEntity()
        {
            ClientName = orderDto.ClientName,
            OrderStatus = orderDto.Status,
            PaymentType = orderDto.PaymentType,
            OrderItems = orderDto.OrderItems?.ToListEntity()
        };
    }

    public static UpdateOrderDto ToUpdateDto(this OrderEntity entity) 
    {
        return new UpdateOrderDto()
        {
            ClientName = entity.ClientName,
            Status = entity.OrderStatus,
            PaymentType = entity.PaymentType,
            OrderItems = entity.OrderItems?.ToListDto()
        };
    }

    public static OrderEntity ToEntity(this UpdateOrderDto updateOrderDto) 
    {
        return new OrderEntity()
        {
            ClientName = updateOrderDto.ClientName!,
            OrderStatus = updateOrderDto.Status!.Value,
            PaymentType = updateOrderDto.PaymentType!.Value,
            UpdatedAt = DateTime.UtcNow,
            OrderItems = updateOrderDto?.OrderItems!.ToListEntity()
        };
    }
}
