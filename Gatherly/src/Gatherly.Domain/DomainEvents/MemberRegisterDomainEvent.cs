using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents;

public record MemberRegisteredDomainEvent(Guid MemberId) : IDomainEvent;