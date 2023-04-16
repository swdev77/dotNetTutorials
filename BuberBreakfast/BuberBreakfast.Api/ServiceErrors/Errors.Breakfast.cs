using ErrorOr;

namespace BuberBreakfast.Api.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NameRequired => Error.Validation(
            code: "Breakfast.NameRequired",
            description: "Breakfast name is required");
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be at least {Model.Breakfast.MinNameLength} "+
                         $"and at most {Model.Breakfast.MaxNameLength} characters long");
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found");
    }
}