using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement
{
    [DependsOn(
        typeof(AbpOcelotManagementApplicationModule),
        typeof(AbpOcelotManagementDomainTestModule)
        )]
    public class AbpOcelotManagementApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
        }
    }
}
