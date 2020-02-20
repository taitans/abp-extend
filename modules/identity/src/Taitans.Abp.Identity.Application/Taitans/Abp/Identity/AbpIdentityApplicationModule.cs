using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Taitans.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(Volo.Abp.Identity.AbpIdentityApplicationModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpIdentityApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpIdentityApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<IdentityApplicationAutoMapperProfile>();
            });
        }
    }
}
