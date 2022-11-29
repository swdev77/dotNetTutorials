namespace Gatherly.Domain.Exceptions;

public sealed class GatheringMaximumNumberOfAttendeesIsNullDomainException : DomainException
{
    public GatheringMaximumNumberOfAttendeesIsNullDomainException() : base("Maximum number of attendees is null")
    {
    }
}