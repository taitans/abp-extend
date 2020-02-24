using Taitans.Abp.OcelotManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement
{
    [DependsOn(
        typeof(AbpOcelotManagementEntityFrameworkCoreTestModule),
        typeof(AbpOcelotManagementTestBaseModule)
        )]
    public class AbpOcelotManagementDomainTestModule : AbpModule
    {

    }
}
