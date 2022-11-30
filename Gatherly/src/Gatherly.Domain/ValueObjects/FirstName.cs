using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;
    private FirstName(string value)
    {
        Value = value;
    }
    public string Value { get; }
    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<FirstName>(new Error(
                "FirstName.Empty",
                "First name is required."));
        }
        if (value.Length > MaxLength)
        {
            return Result.Failure<FirstName>(new Error(
                "FirstName.TooLong",
                $"First name must be less than {MaxLength} characters."));
        }
        return new FirstName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}