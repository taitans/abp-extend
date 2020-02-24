using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement
{
    [DependsOn(
        typeof(AbpOcelotManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class AbpOcelotManagementApplicationContractsModule : AbpModule
    {
    }
}
