using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

public class GatheringRepository : IGatheringRepository
{
    private readonly ApplicationDbContext _context;

    public GatheringRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Gathering gathering)
    {
        _context.Set<Gathering>().Add(gathering);
    }

    public async Task<Gathering?> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Gathering>()
            .Include(g => g.Creator)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<Gathering?> GetByIdWithInvitationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Gathering>()
            .Include(g => g.Invitations)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public void Remove(Gathering gathering)
    {
        _context.Set<Gathering>().Remove(gathering);
    }
}
