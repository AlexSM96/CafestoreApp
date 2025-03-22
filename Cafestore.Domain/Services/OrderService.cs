
using Cafestore.Domain.Models.Mappers;

namespace Cafestore.Domain.Services;

public class OrderService(ICafestoreDbContext context) : IOrderService
{
    private readonly ICafestoreDbContext _context = context;

    public async Task<IEnumerable<OrderDto>> GetOrders(OrderFilter filter)
    {
        return await _context.Orders
            .AsNoTracking()
            .Select(e => e.ToDto())
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
            throw new InvalidOperationException("В заказе отсутсвуеют товары");
        }

        var addResult = await _context.Orders.AddAsync(orderDto.ToEntity());
        await _context.SaveChangesAsync();

        return addResult.Entity.ToDto();
    }

}
