using System.ComponentModel.Design;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities;

public sealed class Invitation : Entity
{
    internal Invitation(Guid id, Member member, Gathering gathering)
        : base(id)
    {
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.UtcNow;
    }

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