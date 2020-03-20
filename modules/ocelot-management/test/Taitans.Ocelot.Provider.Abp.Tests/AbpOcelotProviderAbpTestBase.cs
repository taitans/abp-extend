using Taitans.Abp.OcelotManagement;
using Volo.Abp;
using Volo.Abp.EventBus.Distributed;

namespace Taitans.Ocelot.Provider.Abp.Tests
{
    public abstract class AbpOcelotProviderAbpTestBase : OcelotManagementTestBase<AbpOcelotProviderAbpTestModule>
    {
        protected IDistributedEventBus DistributedEventBus;

        protected AbpOcelotProviderAbpTestBase()
        {
            DistributedEventBus = GetRequiredService<LocalDistributedEventBus>();
        }

        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
