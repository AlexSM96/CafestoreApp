namespace Cafestore.WebAPI.Controllers;

[Route("orders")]
public class OrderController(IOrderService orderService) : ApiBaseContorller
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet("get")]
    public async Task<IActionResult> GetOrders([FromQuery] OrderFilter orderFilter)
    {
        var orders = await _orderService.GetOrders(orderFilter);    
        return Ok(new { Orders = orders });
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        var addResult = await _orderService.CreateOrder(orderDto);
        return Ok(new { CreatedOrder = addResult });
    }
} 
