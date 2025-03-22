namespace Cafestore.WebAPI.Controllers;

[Route("cafestore")]
public class CafestoreController : ApiBaseContorller
{
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(new {});
    }
}
