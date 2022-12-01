using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(
    Guid GatheringId,
    Guid InvitationId) : IDomainEvent;