using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Taitans.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpHttpClientModule))]
    public class AbpIdentityHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Identity";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpIdentityApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
