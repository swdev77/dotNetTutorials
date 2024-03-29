using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost()]
    public IActionResult CreateBreakfast([FromBody] CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        var createdResult = _breakfastService.CreateBreakfast(breakfast);

        return createdResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getResult = _breakfastService.GetBreakfast(id);

        return getResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, [FromBody] UpsertBreakfastRequest request)
    {
        var requestToBreakfastResult = Breakfast.From(id, request);

        if(requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        var upsertedResult = _breakfastService.UpsertBreakfast(breakfast);

        return upsertedResult.Match(
            upsertedBreakfast => upsertedBreakfast.IsNewlyCreated
                ? CreatedAtGetBreakfast(breakfast)
                : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        var deletedResult = _breakfastService.DeleteBreakfast(id);
        return deletedResult.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }

    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet);
    }
    private IActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast));
    }

}