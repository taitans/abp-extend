using Taitans.Abp.OcelotManagement;
using Taitans.Abp.OcelotManagement.EntityFrameworkCore;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Taitans.Ocelot.Provider.Abp.Tests
{
    [DependsOn(
        typeof(AbpOcelotManagementTestBaseModule),
        typeof(AbpOcelotManagementEntityFrameworkCoreTestModule),
        typeof(AbpEventBusModule)
    )]
    public class AbpOcelotProviderAbpTestModule : AbpModule
    {
    }
}
