using Breakfast.ServiceErrors;
using ErrorOr;

namespace Breakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Models.Breakfast> ibreakfasts = new();
    public ErrorOr<Created> CreateBreakfast(Models.Breakfast breakfast)
    {
        ibreakfasts.Add( breakfast.Id, breakfast);

        return Result.Created;
        
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        ibreakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Models.Breakfast> GetBreakfast(Guid id)
    {
       if( ibreakfasts.TryGetValue(id, out var breakfast))
       {
        return breakfast;
       }

       return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Models.Breakfast breakfast)
    {
        var isNewlyCreated = !ibreakfasts.ContainsKey(breakfast.Id);
       ibreakfasts[breakfast.Id] = breakfast;

       return new UpsertedBreakfast(isNewlyCreated);
    }
}