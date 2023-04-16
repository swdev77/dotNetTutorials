using BuberBreakfast.Api.ServiceErrors;
using BuberBreakfast.Contracts.Breakfast;
using ErrorOr;

namespace BuberBreakfast.Api.Model;

public class Breakfast
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savory,
        List<string> sweet, 
        Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Errors.Breakfast.NameRequired;
        }

        if (name.Length < MinNameLength || name.Length > MaxNameLength)
        {
            return Errors.Breakfast.InvalidName;
        }

        // if (string.IsNullOrWhiteSpace(description))
        // {
        //     return Errors.Breakfast.DescriptionRequired;
        // }

        // if (startDateTime == default)
        // {
        //     return Errors.Breakfast.StartDateTimeRequired;
        // }

        // if (endDateTime == default)
        // {
        //     return Errors.Breakfast.EndDateTimeRequired;
        // }

        // if (lastModifiedDateTime == default)
        // {
        //     return Errors.Breakfast.LastModifiedDateTimeRequired;
        // }

        // if (savory == default)
        // {
        //     return Errors.Breakfast.SavoryRequired;
        // }

        // if (sweet == default)
        // {
        //     return Errors.Breakfast.SweetRequired;
        // }

        return new Breakfast(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            savory,
            sweet);
    }

    public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet);
    }

    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet,
            id);
    }
}