namespace Breakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Models.Breakfast> ibreakfasts = new();
    public void CreateBreakfast(Models.Breakfast breakfast)
    {
        ibreakfasts.Add( breakfast.Id, breakfast);
        
    }

    public Models.Breakfast GetBreakfast(Guid id)
    {
        return ibreakfasts[id];
    }
}