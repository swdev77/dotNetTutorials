using BuberBreakfast.Api.Model;
using BuberBreakfast.Api.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Api.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfasts)
    {
        _breakfasts.Add(breakfasts.Id, breakfasts);
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;

    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewly = !_breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[breakfast.Id] = breakfast;
        return new UpsertedBreakfast(isNewly);
    }
}