using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Taitans.Abp.SaasManagement
{
    public class TenantManager : DomainService, ITenantManager
    {
        private readonly ITenantRepository _saasTenantRepository;
        private readonly IEditionRepository _editionRepository;

        public TenantManager(ITenantRepository saasTenantRepository,
            IEditionRepository editionRepository)
        {
            _saasTenantRepository = saasTenantRepository;
            _editionRepository = editionRepository;
        }

        public async Task<List<Tenant>> GetListAsync(bool includDetails = false)
        {
            return await _saasTenantRepository.GetListAsync(includDetails);
        }

        public async Task<Tenant> CreateAsync(string name, string displayName, Guid? editionId)
        {
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name);
            await ValidateEditionAsync(editionId);
            return new Tenant(GuidGenerator.Create(), name, displayName, editionId, CurrentTenant.Id);
        }

        public async Task ChangeNameAsync(Tenant tenant, string name)
        {
            Check.NotNull(tenant, nameof(tenant));
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name, tenant.Id);
            tenant.SetName(name);
        }

        protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var tenant = await _saasTenantRepository.FindByNameAsync(name);
            if (tenant != null && tenant.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate tenancy name: " + name); //TODO: A domain exception would be better..?
            }
        }

        public async Task SetEdition(Tenant tenant, Guid? editionId)
        {
            Check.NotNull(tenant, nameof(tenant));

            await ValidateEditionAsync(editionId);
            tenant.ChangeEditionId(editionId);
        }

        private async Task ValidateEditionAsync(Guid? expectedId)
        {
            if (expectedId.HasValue)
            {
                var edition = await _editionRepository.FindAsync(expectedId.Value);
                if (edition == null)
                {
                    throw new UserFriendlyException("Not Found EditionId: " + expectedId.Value); //TODO: A domain exception would be better..?
                }
            }
        }
    }
}
