using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Taitans.Abp.SaasManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Taitans.Abp.SaasManagement
{
    [DependsOn(
        typeof(AbpSaasManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpSaasManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpSaasManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AbpSaasManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
