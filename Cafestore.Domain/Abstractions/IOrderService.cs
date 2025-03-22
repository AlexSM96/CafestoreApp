namespace Cafestore.Domain.Abstractions;

public interface IOrderService
{
    public Task<IEnumerable<OrderDto>> GetOrders(OrderFilter filter);

    public Task<OrderDto> CreateOrder(CreateOrderDto orderDto);

    public Task<OrderDto> UpdateOrder(long orderId, JsonPatchDocument<UpdateOrderDto> orderDto);
}
