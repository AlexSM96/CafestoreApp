namespace Cafestore.WebAPI.Controllers;

[Route("assortment")]
public class AssortmentController(IAssortmentService assortmentService) : ApiBaseContorller
{
    private readonly IAssortmentService _assortmentService = assortmentService;

    [HttpGet("get")]
    public async Task<IActionResult> GetAssortmentItems()
    {
        var assortmentItems = await _assortmentService.GetAssortments();
        return Ok(new { AssortmentItems = assortmentItems });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAssortmentItem([FromBody] CreateAssortmentItem assortmentItem)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var newAsortmentItem = await _assortmentService.AddAssortmentItem(assortmentItem);
        if(newAsortmentItem is null)
        {
            return Empty;
        }

        return Ok(new { NewAssortmentItem = newAsortmentItem });
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAssortmentItem(long assortmentItemId)
    {
        await _assortmentService.DeleteAssortmentItem(assortmentItemId);
        return Ok();
    }
}
