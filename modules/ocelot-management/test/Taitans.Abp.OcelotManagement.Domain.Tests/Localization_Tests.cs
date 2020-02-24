using Microsoft.Extensions.Localization;
using Shouldly;
using Taitans.Abp.OcelotManagement.Localization;
using Xunit;

namespace Taitans.Abp.OcelotManagement
{
    public class Localization_Tests : AbpOcelotManagementDomainTestBase
    {
        private readonly IStringLocalizer<AbpOcelotManagementResource> _stringLocalizer;

        public Localization_Tests()
        {
            _stringLocalizer = GetRequiredService<IStringLocalizer<AbpOcelotManagementResource>>();
        }

        [Fact]
        public void Test()
        {
            _stringLocalizer["Permission:OcelotManagement"].Value.ShouldBe("Ocelot management");
        }
    }
}
