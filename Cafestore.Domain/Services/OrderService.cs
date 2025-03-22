namespace Cafestore.Domain.Services;

public class OrderService(ICafestoreDbContext context) : IOrderService
{
    private readonly ICafestoreDbContext _context = context;

    public async Task<IEnumerable<OrderDto>> GetOrders(OrderFilter filter)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(order => order.OrderItems)
            .Filter(filter)
            .Select(order => order.ToDto())
            .ToListAsync();
    }

    public async Task<OrderDto> CreateOrder(CreateOrderDto orderDto)
    {
        if (orderDto == null) 
        {
            throw new ArgumentNullException(nameof(orderDto));
        }

        if(orderDto.OrderItems == null || !orderDto.OrderItems.Any())
        {
            throw new EmptyOrderException("В заказе отсутсвуеют товары");
        }

        foreach (var item in orderDto.OrderItems)
        {
            if(!await _context.AssortmentItems.AnyAsync(x => x.Name == item.Name))
            {
                throw new EntityNotFoundException("В ассортименте отсутствуют товары из заказа");
            }
        }
        
        var addResult = await _context.Orders.AddAsync(orderDto.ToEntity());
        await _context.SaveChangesAsync();

        return addResult.Entity.ToDto();
    }

    public async Task<OrderDto> UpdateOrder(long orderId, JsonPatchDocument<UpdateOrderDto> patchOrderDto)
    {
        var order = await _context.Orders.FindAsync(patchOrderDto);
        if (order is null)
        {
            throw new EntityNotFoundException($"Сущность {nameof(OrderEntity)} c Id {orderId} не существует");
        }

        var updatedDto = order.ToUpdateDto();
        patchOrderDto.ApplyTo(updatedDto);

        var updateResult = _context.Orders.Update(updatedDto.ToEntity());
        await _context.SaveChangesAsync();

        return updateResult.Entity.ToDto();
    }
}
