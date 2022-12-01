using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;
using MediatR;

namespace Gatherly.Application.Members.Commands;

public sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        var emailResult = Email.Create(request.Email);

        if (firstNameResult.IsFailure || lastNameResult.IsFailure || emailResult.IsFailure)
        {
            return Result.Failure<Guid>(new("InvalidData", "Invalid member data."));
        }

        if (!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.Member.EmailAlreadyInUse);
        }

        var member = Member.Create(
            Guid.NewGuid(),
            firstNameResult.Value!,
            lastNameResult.Value!,
            emailResult.Value!);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return member.Id;
    }

    Task<Unit> IRequestHandler<CreateMemberCommand, Unit>.Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}