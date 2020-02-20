using Volo.Abp.Modularity;

namespace Taitans.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(AbpIdentityDomainTestModule)
        )]
    public class AbpIdentityApplicationTestModule : AbpModule
    {

    }
}
