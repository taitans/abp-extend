using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpOcelotManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class AbpOcelotManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OcelotManagementDbContext>(options =>
            {
                options.AddRepository<Ocelot, EfCoreOcelotRepository>();
            });
        }
    }
}