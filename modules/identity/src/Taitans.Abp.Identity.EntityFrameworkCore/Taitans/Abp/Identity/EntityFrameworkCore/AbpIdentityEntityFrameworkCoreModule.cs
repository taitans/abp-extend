using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using IdentityClaimType = Volo.Abp.Identity.IdentityClaimType;

namespace Taitans.Abp.Identity.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(Volo.Abp.Identity.EntityFrameworkCore.AbpIdentityEntityFrameworkCoreModule)
    )]
    public class AbpIdentityEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<IdentityDbContext>(options =>
            {
                options.AddRepository<IdentityClaimType, EfCoreIdentityClaimTypeRepository>();
            });
        }
    }
}