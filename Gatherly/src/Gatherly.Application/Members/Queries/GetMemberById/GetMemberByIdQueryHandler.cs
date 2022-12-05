using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMemberById;

public sealed class GetMemberByIdQueryHandler
    : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
        {
            return Result.Failure<MemberResponse>(new Error("Member.NotFound", $"Member with id {request.Id} not found"));
        }

        var response = new MemberResponse(member.Id, member.Email.Value);

        return response;
    }
}