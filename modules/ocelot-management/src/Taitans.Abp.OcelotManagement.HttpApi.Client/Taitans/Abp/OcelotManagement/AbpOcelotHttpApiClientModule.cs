using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement
{
    [DependsOn(
        typeof(AbpOcelotManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class AbpOcelotHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Ocelot";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpOcelotManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
