using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.Identity.MongoDB
{
    [DependsOn(
        typeof(Volo.Abp.Identity.MongoDB.AbpIdentityMongoDbModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class AbpIdentityMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<AbpIdentityMongoDbContext>(options =>
            {
                options.AddRepository<IdentityClaimType, MongoIdentityClaimTypeRepository>();
            });
        }
    }
}
