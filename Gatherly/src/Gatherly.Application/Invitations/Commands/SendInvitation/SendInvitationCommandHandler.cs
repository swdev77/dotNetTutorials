using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public SendInvitationCommandHandler(
        IUnitOfWork unitOfWork,
        IMemberRepository memberRepository,
        IGatheringRepository gatheringRepository,
        IInvitationRepository invitationRepository,
        IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _invitationRepository = invitationRepository;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (member == null || gathering == null)
        {
            return Unit.Value;
        }

        var invitationResult = gathering.SendInvitation(member);

        if (invitationResult.IsFailure)
        {
            return Unit.Value;
        }

        _invitationRepository.Add(invitationResult.Value!);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendInvitationSendEmailAsync(invitationResult.Value!, cancellationToken);

        return Unit.Value;
    }
}