namespace Breakfast.Services.Breakfasts;

using System;
using Breakfast.Models;

public interface IBreakfastService
{
    void CreateBreakfast(Breakfast request);
    Breakfast GetBreakfast(Guid id);
}