using Volo.Abp.Modularity;

namespace Taitans.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainSharedModule),
        typeof(Volo.Abp.Identity.AbpIdentityDomainModule)
        )]
    public class AbpIdentityDomainModule : AbpModule
    {

    }
}
