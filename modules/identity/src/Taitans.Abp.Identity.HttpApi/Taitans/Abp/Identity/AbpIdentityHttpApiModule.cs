using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Taitans.Abp.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Taitans.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityApplicationContractsModule),
        typeof(Volo.Abp.Identity.AbpIdentityHttpApiModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpIdentityHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpIdentityHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<IdentityResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
