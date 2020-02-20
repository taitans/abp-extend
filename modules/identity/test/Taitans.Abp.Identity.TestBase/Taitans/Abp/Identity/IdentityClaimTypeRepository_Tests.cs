using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Taitans.Abp.Identity
{
    public abstract class IdentityClaimTypeRepository_Tests<TStartupModule> :
        AbpIdentityTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        protected IIdentityClaimTypeRepository ClaimTypeRepository { get; }

        public IdentityClaimTypeRepository_Tests()
        {
            ClaimTypeRepository = ServiceProvider.GetRequiredService<IIdentityClaimTypeRepository>();
        }

        [Fact]
        public async Task GetCountAsync()
        {
            var count = await ClaimTypeRepository.GetCountAsync().ConfigureAwait(false);

            count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetClaimTypesAsync()
        {
            var claimTypes = await ClaimTypeRepository.GetClaimTypesAsync();
            claimTypes.ShouldNotBeNull();
            claimTypes.ShouldContain("Taitans");
        }
    }
}
