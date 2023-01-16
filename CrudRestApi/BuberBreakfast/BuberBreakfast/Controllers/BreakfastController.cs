using BuberBreakfast.Contracts.Breakfast;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[ApiController]
public class BreakfastController : ControllerBase
{
    [HttpPost("/breakfast")]
    public IActionResult CreateBreakfast([FromBody] CreateBreakfastRequest request)
    {
        return Ok(request);
    }

    [HttpGet("/breakfast/{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        return Ok(id);
    }

    [HttpPut("/breakfast/{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, [FromBody] UpsertBreakfastRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("/breakfast/{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        return Ok(id);
    }
}