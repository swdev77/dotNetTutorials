namespace Gatherly.Domain.Entities;

public class Invitation
{
    internal Invitation(
        Guid id, Member member, Gathering gathering)
    {
        Id = id;
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid Id { get; }
    public Guid MemberId { get; }
    public Guid GatheringId { get; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime ModifiedOnUtc { get; private set; }

    internal Attendee Accept()
    {
        Status = InvitationStatus.Accepted;
        ModifiedOnUtc = DateTime.UtcNow;
        var attendee = new Attendee(this);
        return attendee;
    }

    internal void Expire()
    {
        Status = InvitationStatus.Expired;
        ModifiedOnUtc = DateTime.UtcNow;
    }
}