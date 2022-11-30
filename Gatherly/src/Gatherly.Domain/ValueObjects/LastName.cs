using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 50;
    public LastName(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<LastName>(new Error(
                "LastName.Empty",
                "Last name is required."));
        }
        if (value.Length > MaxLength)
        {
            return Result.Failure<LastName>(new Error(
                "LastName.TooLong",
                $"Last name must be less than {MaxLength} characters."));
        }
        return new LastName(value);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}