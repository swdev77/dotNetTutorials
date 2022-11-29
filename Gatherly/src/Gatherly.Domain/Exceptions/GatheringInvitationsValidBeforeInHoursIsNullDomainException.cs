namespace Gatherly.Domain.Exceptions;

public sealed class GatheringInvitationsValidBeforeInHoursIsNullDomainException : DomainException
{
    public GatheringInvitationsValidBeforeInHoursIsNullDomainException() : base("Invitations valid before in hours is null")
    {
    }
}