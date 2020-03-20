using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Taitans.Abp.SaasManagement
{
    public class EditionManager : DomainService, IEditionManager
    {
        private readonly IEditionRepository _editionRepository;

        public EditionManager(IEditionRepository editionRepository)
        {
            _editionRepository = editionRepository;
        }

        public async Task ChangeNameAsync([NotNull] Edition tenant, [NotNull] string displayName)
        {
            Check.NotNull(tenant, nameof(tenant));
            Check.NotNull(displayName, nameof(displayName));

            await ValidateDisplayNameAsync(displayName, tenant.Id);
            tenant.SetDisplayName(displayName);
        }

        public async Task<Edition> CreateAsync([NotNull] string displayName)
        {
            Check.NotNull(displayName, nameof(displayName));

            await ValidateDisplayNameAsync(displayName);
            return new Edition(GuidGenerator.Create(), displayName, CurrentTenant.Id);
        }

        public Task<Edition> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Edition>> GetAllAsync()
        {
            return await _editionRepository.GetListAsync();
        }

        public async Task<Edition> GetByIdAsync(Guid id)
        {
            return await _editionRepository.GetAsync(id);
        }

        public async Task<Edition> GetDefaultAsync()
        {
            return await _editionRepository.GetDefaultAsync();
        }

        protected virtual async Task ValidateDisplayNameAsync(string name, Guid? expectedId = null)
        {
            var edition = await _editionRepository.FindByNameAsync(name);
            if (edition != null && edition.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate tenancy name: " + name); //TODO: A domain exception would be better..?
            }
        }
    }
}
