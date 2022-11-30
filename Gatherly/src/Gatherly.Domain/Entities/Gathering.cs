using System.Collections.ObjectModel;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Entities;

public sealed class Gathering : AggregateRoot
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
        int? invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(
            id,
            creator,
            type,
            scheduledAtUtc,
            name,
            location
        );

        gathering.CalculateGatheringTypeDetails(
            maximumNumberOfAttendees,
            invitationsValidBeforeInHours
        );

        return gathering;
    }

    private void CalculateGatheringTypeDetails(
        int? maximumNumberOfAttendees,
        int? invitationsValidBeforeInHours)
    {
        switch (Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    throw new GatheringMaximumNumberOfAttendeesIsNullDomainException();
                }
                MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;

            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new GatheringInvitationsValidBeforeInHoursIsNullDomainException();
                }
                InvitationExpireAtUtc = ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
    }

    public Result<Invitation> SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.AlreadyPassed);
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