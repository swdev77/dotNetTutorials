using Gatherly.Application.Abstractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Members.Events;

public sealed class MemberRegisteredDomainEventHandler : INotificationHandler<MemberRegisteredDomainEvent>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IEmailService _emailService;

    public MemberRegisteredDomainEventHandler(IEmailService emailService, IMemberRepository memberRepository)
    {
        _emailService = emailService;
        _memberRepository = memberRepository;
    }

    public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);

        if (member == null)
        {
            return;
        }

        await _emailService.SendWelcomeEmailAsync(member, cancellationToken);
    }
}