using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.OcelotManagement.MongoDB
{
    [DependsOn(
        typeof(AbpOcelotManagementDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class AbpOcelotManagementMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<AbpOcelotManagementMongoDbContext>(options =>
            {
                options.AddDefaultRepositories<IAbpOcelotManagementMongoDbContext>();

                options.AddRepository<Ocelot, MongoOcelotRepository>();
            });
        }
    }
}
