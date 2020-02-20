using Microsoft.Extensions.Localization;
using Shouldly;
using Taitans.Abp.Identity.Localization;
using Xunit;

namespace Taitans.Abp.Identity
{
    public class Localization_Tests : AbpIdentityDomainTestBase
    {
        private readonly IStringLocalizer<IdentityResource> _stringLocalizer;

        public Localization_Tests()
        {
            _stringLocalizer = GetRequiredService<IStringLocalizer<IdentityResource>>();
        }

        [Fact]
        public void Test()
        {
            _stringLocalizer["Permission:ClaimManagement"].Value
            .ShouldBe("Claim Management");
        }
    }
}
