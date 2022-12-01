using Gatherly.Domain.Entities;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Repositories;
public interface IMemberRepository
{
    void Add(Member member);
    Task<Member> GetByIdAsync(Guid memberId, CancellationToken cancellationToken);
    Task<bool> IsEmailUniqueAsync(Email? value, CancellationToken cancellationToken);
}