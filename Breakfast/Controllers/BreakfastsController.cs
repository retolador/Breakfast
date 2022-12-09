using Microsoft.AspNetCore.Mvc;
using Breakfast.Contracts.Breakfasts;
using Breakfast.Models;
using Breakfast.Services.Breakfasts;

namespace Breakfast.Controllers;

[ApiController]
[Route("breakfasts")]
//[Route("[controller]")]
public class BreakfastController : ControllerBase
{
    private readonly IBreakfastService ibreakfastService;

    public BreakfastController(IBreakfastService breakfastService)
    {
        ibreakfastService = breakfastService;
    }
    [HttpPost("")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Models.Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);

       ibreakfastService.CreateBreakfast(breakfast);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id},
            value: response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        Models.Breakfast breakfast = ibreakfastService.GetBreakfast(id);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, CreateBreakfastRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        return Ok(id);
    }
}