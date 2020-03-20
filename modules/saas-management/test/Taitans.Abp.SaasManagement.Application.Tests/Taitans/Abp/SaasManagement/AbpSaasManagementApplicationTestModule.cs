using Volo.Abp.Modularity;

namespace Taitans.Abp.SaasManagement
{
    [DependsOn(
        typeof(AbpSaasManagementApplicationModule),
        typeof(AbpSaasManagementDomainTestModule)
        )]
    public class AbpSaasManagementApplicationTestModule : AbpModule
    {

    }
}
