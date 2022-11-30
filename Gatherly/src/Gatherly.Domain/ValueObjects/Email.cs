using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 50;
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Email>(new Error(
                "Email.Empty",
                "Email is required."));
        }
        if (!value.Contains("@"))
        {
            return Result.Failure<Email>(new Error(
                "Email.Invalid",
                "Email is invalid."));
        }
        if (value.Length > MaxLength)
        {
            return Result.Failure<Email>(new Error(
                "Email.TooLong",
                $"Email must be less than {MaxLength} characters."));
        }

        return new Email(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}