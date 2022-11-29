using System.Collections.ObjectModel;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities;

public sealed class Gathering : Entity
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();
    private Gathering(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime scheduledAt,
        string name,
        string? location
    ) : base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAt;
        Name = name;
        Location = location;
    }
    public Member Creator { get; }
    public GatheringType Type { get; }
    public DateTime ScheduledAtUtc { get; }
    public string Name { get; }
    public string? Location { get; }
    public int? MaximumNumberOfAttendees { get; private set; }
    public DateTime? InvitationExpireAtUtc { get; private set; }
    public int? NumberOfAttendees { get; private set; }
    public IReadOnlyCollection<Attendee> Attendees => _attendees;
    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    public static Gathering Create(
        Guid id,
        Member creator,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string? location,
        int? maximumNumberOfAttendees,
        int? invitationsValidBeforeInHours )
    {
        var gathering = new Gathering(
            id,
            creator,
            type,
            scheduledAtUtc,
            name,
            location
        );

        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    throw new Exception($"{nameof(maximumNumberOfAttendees)} can't be null.");
                }
                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;

            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new Exception($"{nameof(invitationsValidBeforeInHours)} can't be null.");
                }
                gathering.InvitationExpireAtUtc = gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }

        return gathering;
    }

    public Invitation SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
        {
            throw new Exception("Can't send invitation to the gathering creator.");
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            throw new Exception("Can't send invitation for gathering in the past.");
        }

        var invitation = new Invitation(Guid.NewGuid(), member, this);
        _invitations.Add(invitation);
        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
       var expired = (Type == GatheringType.WithFixedNumberOfAttendees &&
                       NumberOfAttendees == MaximumNumberOfAttendees) ||
                      (Type == GatheringType.WithExpirationForInvitations &&
                       InvitationExpireAtUtc < DateTime.UtcNow);

        if (expired)
        {
            invitation.Expire();
            return null;
        }

        var attendee = invitation.Accept();
        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}