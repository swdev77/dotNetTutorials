using System.Threading.Tasks;
using Gatherly.Domain.Primitives;
using Gatherly.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Gatherly.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor :
    SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}