using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class DomainErrors
{
    public static class Gathering
    {
        public static readonly Error GatheringNotFound =
            new ("Gathering.GatheringNotFound", "The gathering was not found.");
        public static readonly Error InvitingCreator =
            new ("Gathering.InvitingCreator", "You cannot invite yourself to a gathering.");

        public static readonly Error AlreadyPassed =
            new ("Gathering.AlreadyPassed", "The gathering has already passed.");

        public static readonly Error InvitationExpired =
            new ("Gathering.InvitationExpired", "The invitation has expired.");
    }

    public static class Member
    {
        public static readonly Error EmailAlreadyInUse =
            new("Member.EmailAlreadyInUse", "The email is already in use.");
    }

    public static class Invitation 
    {
        public static readonly Error AlreadyAccepted =
            new ("Invitation.AlreadyAccepted", "The invitation has already been accepted.");
        public static readonly Error AlreadyDeclined =
            new ("Invitation.AlreadyDeclined", "The invitation has already been declined.");
        public static readonly Error AlreadyExpired =
            new ("Invitation.AlreadyExpired", "The invitation has already expired.");
    }
}