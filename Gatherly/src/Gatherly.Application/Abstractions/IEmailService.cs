using Gatherly.Domain.Entities;

namespace Gatherly.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationAcceptEmailAsync(Invitation invitation, CancellationToken cancellationToken);
    Task SendInvitationSendEmailAsync(Invitation invitation, CancellationToken cancellationToken);
}