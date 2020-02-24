using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Taitans.Abp.OcelotManagement
{
    public interface IOcelotRepository : IRepository<Ocelot, Guid>
    {
        Task<IList<OcelotReRoute>> GetReRoutesAsync(Guid id);

        Task<Ocelot> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);
    }
}
