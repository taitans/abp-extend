using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpOcelotManagementDomainSharedModule)
        )]
    public class AbpOcelotManagementDomainModule : AbpModule
    {

    }
}
