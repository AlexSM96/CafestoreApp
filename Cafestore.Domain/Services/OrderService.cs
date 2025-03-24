namespace Cafestore.Domain.Services;

public class OrderService(ICafestoreDbContext context) : IOrderService
{
    private readonly ICafestoreDbContext _context = context;

    public async Task<IEnumerable<OrderDto>> GetOrders(OrderFilter filter)
    {
        return await _context.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Filter(filter)
            .Select(order => order.ToDto())
            .ToListAsync();
    }

    public async Task<OrderDto> CreateOrder(CreateOrderDto orderDto)
    {
        if (orderDto is null)
        {
            throw new ArgumentNullException(nameof(orderDto));
        }

        if (orderDto.OrderItems is null || !orderDto.OrderItems.Any())
        {
            throw new EmptyOrderException("В заказе отсутсвуеют товары");
        }

        var assortmentItems = await GetAssortementByOrderItems(orderDto.OrderItems.Select(x => x.Id));
        var addResult = await _context.Orders.AddAsync(orderDto.ToEntity(assortmentItems));
        await _context.SaveChangesAsync();

        return addResult.Entity.ToDto();
    }

    public async Task<OrderDto> UpdateOrder(long orderId, JsonPatchDocument<UpdateOrderDto> patchOrderDto)
    {
        var order = await _context.Orders
            .Include(e => e.OrderItems)
            .FirstOrDefaultAsync(e => e.Id == orderId);
    
        if (order is null)
        {
            throw new EntityNotFoundException($"Сущность {nameof(OrderEntity)} c Id {orderId} не существует");
        }

        var updatedDto = order.ToUpdateDto();
        patchOrderDto.ApplyTo(updatedDto);

        if (updatedDto.OrderItems is null || !updatedDto.OrderItems.Any())
        {
            throw new EmptyOrderException("В заказе отсутсвуеют товары");
        }

        if(order.OrderStatus == OrderStatus.Completed && updatedDto.Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Нельзя \"отменить\" выполненный заказ");
        }

        if(order.OrderStatus == OrderStatus.Cancelled && updatedDto.Status == OrderStatus.Completed)
        {
            throw new InvalidOperationException("Нельзя \"выполнить\" отменённый заказ");
        }

        var assortmentItems = await GetAssortementByOrderItems(updatedDto.OrderItems.Select(x => x.Id));
        order.ClientName = updatedDto.ClientName!;
        order.PaymentType = updatedDto.PaymentType!.Value;
        order.OrderStatus = updatedDto.Status!.Value;
        order.UpdatedAt = DateTime.UtcNow;
        order.OrderItems = assortmentItems.ToList();

        var updateResult = _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return updateResult.Entity.ToDto();
    }


    private async Task<IEnumerable<AssortmentItemEntity>> GetAssortementByOrderItems(IEnumerable<long> itemIds)
    {
        var assortmentItems = new List<AssortmentItemEntity>();
        foreach (var id in itemIds)
        {
            var existedItem = await _context.AssortmentItems.FindAsync(id);
            if (existedItem is null)
            {
                throw new EntityNotFoundException($"Сущность {nameof(AssortmentItemEntity)} c Id {id} не существует");
            }

            assortmentItems.Add(existedItem);
        }

        return assortmentItems;
    }
}
