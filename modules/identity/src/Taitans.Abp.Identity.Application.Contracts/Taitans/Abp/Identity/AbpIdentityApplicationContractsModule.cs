using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Taitans.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(Volo.Abp.Identity.AbpIdentityApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class AbpIdentityApplicationContractsModule : AbpModule
    {
    }
}
