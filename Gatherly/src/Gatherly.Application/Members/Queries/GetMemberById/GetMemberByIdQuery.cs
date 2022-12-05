using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Members.Queries.GetMemberById;

public record GetMemberByIdQuery(Guid Id) : IQuery<MemberResponse>;
