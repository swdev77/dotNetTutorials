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
    }
}