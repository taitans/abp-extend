using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Taitans.Abp.Identity
{
    public interface IIdentityClaimTypeRepository : Volo.Abp.Identity.IIdentityClaimTypeRepository
    {
        Task<long> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default
        );

        Task<List<string>> GetClaimTypesAsync(CancellationToken cancellationToken = default);
    }
}
