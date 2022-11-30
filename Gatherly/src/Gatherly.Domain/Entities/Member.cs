using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : Entity
{
    public Member(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public FirstName FirstName { get; }
    public LastName LastName { get; }
    public Email Email { get; }
}