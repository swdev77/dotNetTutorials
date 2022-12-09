using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : AggregateRoot, IAuditableEntity
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
    public FirstName FirstName { get; private set;}
    public LastName LastName { get; private set; }
    public Email Email { get; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Member Create(
        FirstName firstName,
        LastName lastName,
        Email email)
    {
        var member = new Member(Guid.NewGuid(), firstName, lastName, email);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

        return member;
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}