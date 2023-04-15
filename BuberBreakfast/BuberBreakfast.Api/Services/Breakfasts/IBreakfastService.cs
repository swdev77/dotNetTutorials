using BuberBreakfast.Api.Model;

namespace BuberBreakfast.Api.Services.Breakfasts
{
    public interface IBreakfastService
    {
        void CreateBreakfast(Breakfast breakfasts);
        void DeleteBreakfast(Guid id);
        Breakfast GetBreakfast(Guid id);
        void UpsertBreakfast(Breakfast breakfast);
    }
}