using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : AggregateRoot
{
    private Member(
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

    public static Member Create(
        FirstName firstName,
        LastName lastName,
        Email email)
    {
        var member = new Member(Guid.NewGuid(), firstName, lastName, email);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

        return member;
    }
}