using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Taitans.Abp.SaasManagement
{
    [DependsOn(
        typeof(AbpSaasManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class AbpSaasManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "SaasManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpSaasManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
