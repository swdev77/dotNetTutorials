using Gatherly.Domain.Entities;

namespace Gatherly.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationAcceptEmailAsync(Gathering gathering, CancellationToken cancellationToken);
    Task SendInvitationSendEmailAsync(Invitation invitation, CancellationToken cancellationToken);
    Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken);
}