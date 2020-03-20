using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Taitans.Abp.SaasManagement
{
    public interface IEditionManager : IDomainService
    {
        Task<Edition> CreateAsync([NotNull] string name);

        Task ChangeNameAsync([NotNull] Edition edition, [NotNull] string name);

        Task<Edition> FindByNameAsync(string name);

        Task<List<Edition>> GetAllAsync();

        Task<Edition> GetDefaultAsync();

        Task<Edition> GetByIdAsync(Guid id);
    }
}
