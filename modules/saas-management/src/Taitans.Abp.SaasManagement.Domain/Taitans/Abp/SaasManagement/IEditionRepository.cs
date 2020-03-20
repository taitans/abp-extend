using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Taitans.Abp.SaasManagement
{
    public interface IEditionRepository : IRepository<Edition, Guid>
    {
        Task<Edition> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Edition FindByName(
            string name,
            bool includeDetails = true
        );

        Edition FindById(
            Guid id,
            bool includeDetails = true
        );

        Task<List<Edition>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default);

        Task<Edition> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
