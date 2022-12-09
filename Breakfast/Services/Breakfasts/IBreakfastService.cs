namespace Breakfast.Services.Breakfasts;

using System;
using Breakfast.Models;
using ErrorOr;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast request);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<Updated> UpsertBreakfast(Breakfast breakfast);
}