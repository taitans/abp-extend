using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Taitans.Abp.Identity
{
    public class IdentityClaimTypeAppService_Tests : AbpIdentityApplicationTestBase
    {
        private readonly IIdentityClaimTypeAppService _identityClaimTypeAppService;

        public IdentityClaimTypeAppService_Tests()
        {
            _identityClaimTypeAppService = GetRequiredService<IIdentityClaimTypeAppService>();
        }


        [Fact]
        public async Task GetAll()
        {
            var result = await _identityClaimTypeAppService.GetAll();

            result.Count.ShouldBeGreaterThan(0);
        }


        [Fact]
        public async Task GetListAsync()
        {
            var result = await _identityClaimTypeAppService.GetListAsync(new GetIdentityClaimTypeInput());

            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.Count.ShouldBeGreaterThan(0);
        }
    }
}
