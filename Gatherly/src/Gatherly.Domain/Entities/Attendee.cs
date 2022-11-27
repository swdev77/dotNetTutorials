namespace Gatherly.Domain.Entities;

public class Attendee
{
    internal Attendee(Invitation invitation)
    {
       GatheringId = invitation.GatheringId;
       MemberId = invitation.MemberId;
       CreatedOnUtc = DateTime.UtcNow;
    }
    public Guid MemberId { get; }
    public Guid GatheringId { get; }
    public DateTime CreatedOnUtc { get; }
}