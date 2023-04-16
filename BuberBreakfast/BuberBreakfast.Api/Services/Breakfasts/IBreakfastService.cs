using BuberBreakfast.Api.Model;
using ErrorOr;

namespace BuberBreakfast.Api.Services.Breakfasts
{
    public interface IBreakfastService
    {
        ErrorOr<Created> CreateBreakfast(Breakfast breakfasts);
        ErrorOr<Breakfast> GetBreakfast(Guid id);
        ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
        ErrorOr<Deleted> DeleteBreakfast(Guid id);
    }
}