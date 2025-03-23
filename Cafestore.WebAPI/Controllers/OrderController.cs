namespace Cafestore.WebAPI.Controllers;

[Route("orders")]
public class OrderController(IOrderService orderService) : ApiBaseContorller
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet("get")]
    public async Task<IActionResult> GetOrders([FromQuery] OrderFilter orderFilter)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var orders = await _orderService.GetOrders(orderFilter);    
        return Ok(new { Orders = orders });
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var createdOrder = await _orderService.CreateOrder(orderDto);
        if(createdOrder is null)
        {
            return Empty;
        }

        return Ok(new { CreatedOrder = createdOrder });
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateOrder([FromQuery]long orderId, [FromBody] JsonPatchDocument<UpdateOrderDto> orderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updatedOrder = await _orderService.UpdateOrder(orderId, orderDto);
        if (updatedOrder is null) 
        {
            return Empty;
        }

        return Ok(new { UpdatedOrder = updatedOrder });
    }
} 
