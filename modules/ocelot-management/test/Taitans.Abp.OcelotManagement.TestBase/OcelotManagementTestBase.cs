using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotManagementTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
        where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
