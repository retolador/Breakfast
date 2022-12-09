using Microsoft.AspNetCore.Mvc;
using Breakfast.Contracts.Breakfasts;
using Breakfast.Models;
using Breakfast.Services.Breakfasts;
using static Breakfast.ServiceErrors.Errors;
using ErrorOr;
using Breakfast.ServiceErrors;

namespace Breakfast.Controllers;


//[Route("[controller]")]
public class BreakfastController : ApiController
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

       ErrorOr<Created> createdBreakfastResult = ibreakfastService.CreateBreakfast(breakfast);

       if (createdBreakfastResult.IsError)
       {
         return Problem(createdBreakfastResult.Errors);
       }

       

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id},
            value: MapBreakfastResponse(breakfast));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Models.Breakfast> getBreakfastResult = ibreakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );

/*
        if (getBreakfastResult.IsError &&
        getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
        {
            return NotFound();
        }
        var breakfast = getBreakfastResult.Value;
        BreakfastResponse response = MapBreakfastResponse(breakfast);
        return Ok(response);
        */
    }

  

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, CreateBreakfastRequest request)
    {
         var breakfast = new Models.Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);
        ibreakfastService.UpsertBreakfast(breakfast);

        // TODO: return 201 if a new breakfast was created
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteResult = ibreakfastService.DeleteBreakfast(id);

        return deleteResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
        
    }

      private static BreakfastResponse MapBreakfastResponse(Models.Breakfast breakfast)
    {
        return new BreakfastResponse(
                    breakfast.Id,
                    breakfast.Name,
                    breakfast.Description,
                    breakfast.StartDateTime,
                    breakfast.EndDateTime,
                    breakfast.LastModifiedDateTime,
                    breakfast.Savory,
                    breakfast.Sweet
                );
    }
}