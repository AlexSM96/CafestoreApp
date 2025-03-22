using Cafestore.Domain.Extensions;

namespace Cafestore.Domain.Services;

public class OrderService(ICafestoreDbContext context) : IOrderService
{
    private readonly ICafestoreDbContext _context = context;

    public async Task<IEnumerable<OrderDto>> GetOrders(OrderFilter filter)
    {
        return await _context.Orders
            .AsNoTracking()
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

        if(orderDto.Products == null || !orderDto.Products.Any())
        {
            throw new EmptyOrderException("В заказе отсутсвуеют товары");
        }

        foreach (var item in orderDto.Products)
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
}
