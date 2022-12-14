using Microsoft.AspNetCore.Mvc;
using Breakfast.Contracts.Breakfasts;
using Breakfast.Services.Breakfasts;
using ErrorOr;
using Breakfast.Models;

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
        ErrorOr<Models.Breakfast> requestToBreakfastResult =  Models.Breakfast.From(request);

        if(requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }
        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<Created> createdBreakfastResult = ibreakfastService.CreateBreakfast(breakfast);


/*
        if (createdBreakfastResult.IsError)
        {
            return Problem(createdBreakfastResult.Errors);
        }
*/


        return createdBreakfastResult.Match(
            createdBreakfastResult => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors)
        );
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
         ErrorOr<Models.Breakfast> requestToBreakfastResult = Models.Breakfast.From(id, request);

            if(requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors);
            }
            var breakfast = requestToBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = ibreakfastService.UpsertBreakfast(breakfast);

        // TODO: return 201 if a new breakfast was created
         Console.WriteLine("Obtinguent resultat");
        return upsertBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ?  CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = ibreakfastService.DeleteBreakfast(id);

        
        return deleteBreakfastResult.Match(
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


    private CreatedAtActionResult CreatedAtGetBreakfast(Models.Breakfast breakfast)
    {
        return CreatedAtAction(
                    actionName: nameof(GetBreakfast),
                    routeValues: new { id = breakfast.Id },
                    value: MapBreakfastResponse(breakfast));
    }
}