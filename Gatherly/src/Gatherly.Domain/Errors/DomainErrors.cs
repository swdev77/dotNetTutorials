using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class DomainErrors
{
    public static class Gathering
    {
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
}