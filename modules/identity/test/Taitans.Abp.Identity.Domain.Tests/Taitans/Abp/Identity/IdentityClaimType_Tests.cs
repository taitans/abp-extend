using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Taitans.Abp.Identity
{
    public class IdentityClaimType_Tests : AbpIdentityDomainTestBase
    {
        private readonly IIdentityClaimTypeRepository _identityClaimTypeRepository;

        public IdentityClaimType_Tests()
        {
            _identityClaimTypeRepository = GetRequiredService<IIdentityClaimTypeRepository>();
        }

        [Fact]
        public async Task GetCountAsync()
        {
            var count = await _identityClaimTypeRepository.GetCountAsync().ConfigureAwait(false);

            count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetClaimTypes()
        {
            var claimTypes = await _identityClaimTypeRepository.GetClaimTypesAsync();
            claimTypes.ShouldNotBeNull();
            claimTypes.ShouldContain("Taitans");
        }
    }
}
