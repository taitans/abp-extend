using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Taitans.Abp.SaasManagement
{
    public interface ITenantManager : IDomainService
    {
        Task<List<Tenant>> GetListAsync(bool includDetails = false);

        Task<Tenant> CreateAsync(string name, string displayName, Guid? editionId);

        Task ChangeNameAsync([NotNull] Tenant tenant, [NotNull] string name);

        Task SetEdition(Tenant tenant, Guid? editionId);
    }
}
