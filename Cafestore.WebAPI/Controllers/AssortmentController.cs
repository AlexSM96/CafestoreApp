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
    public async Task<IActionResult> AddAssortmentItem([FromBody] AssortmentItemDto assortmentItem)
    {
        var newAsortmentItem = await _assortmentService.AddAssortmentItem(assortmentItem.Name);
        if(newAsortmentItem is null)
        {
            return Empty;
        }

        return Ok(new { AssortmentItem = newAsortmentItem });
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAssortmentItem(long assortmentItemId)
    {
        await _assortmentService.DeleteAssortmentItem(assortmentItemId);
        return Ok();
    }
}
