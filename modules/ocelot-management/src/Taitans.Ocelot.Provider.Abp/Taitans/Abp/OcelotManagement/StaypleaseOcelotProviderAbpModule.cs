using Microsoft.Extensions.DependencyInjection;
using Taitans.Abp.OcelotManagement.EntityFrameworkCore;
using Taitans.Ocelot.Provider.Abp.Repository;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Taitans.Ocelot.Provider.Abp
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpOcelotManagementEntityFrameworkCoreModule)
    )]
    public class TaitansOcelotProviderAbpModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OcelotManagementDbContext>();

            //Register a factory method that resolves from IServiceProvider
            context.Services.AddScoped<IAbpFileConfigurationRepository>(
                sp => sp.GetRequiredService<EfCoreFileConfigurationRepository>()
            );
        }
    }
}
