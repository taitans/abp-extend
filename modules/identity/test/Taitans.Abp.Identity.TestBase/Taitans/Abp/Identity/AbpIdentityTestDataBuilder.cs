using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Taitans.Abp.Identity
{
    public class AbpIdentityTestDataBuilder : ITransientDependency
    {
        private readonly IIdentityClaimTypeRepository _identityClaimTypeRepository;

        public AbpIdentityTestDataBuilder(
            IIdentityClaimTypeRepository identityClaimTypeRepository)
        {
            _identityClaimTypeRepository = identityClaimTypeRepository;
        }

        public async Task Build()
        {
            await AddClaimTypes();
        }

        private async Task AddClaimTypes()
        {
            var ageClaim = new IdentityClaimType(Guid.NewGuid(), "Age", false, false, null, null, null, IdentityClaimValueType.Int);
            await _identityClaimTypeRepository.InsertAsync(ageClaim);
            var educationClaim = new IdentityClaimType(Guid.NewGuid(), "Education", true, false, null, null, null);
            await _identityClaimTypeRepository.InsertAsync(educationClaim);
            var taitansClaim = new IdentityClaimType(Guid.NewGuid(), "Taitans", true, false, null, null, null, IdentityClaimValueType.DateTime);
            await _identityClaimTypeRepository.InsertAsync(taitansClaim);
        }
    }
}