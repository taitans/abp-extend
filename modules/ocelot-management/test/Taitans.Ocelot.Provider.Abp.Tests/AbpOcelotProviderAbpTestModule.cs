using Taitans.Abp.OcelotManagement;
using Taitans.Abp.OcelotManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Taitans.Ocelot.Provider.Abp.Tests
{
    [DependsOn(
        typeof(AbpOcelotManagementTestBaseModule),
        typeof(AbpOcelotManagementEntityFrameworkCoreTestModule)
    )]
    public class AbpOcelotProviderAbpTestModule : AbpModule
    {
    }
}
