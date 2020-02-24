using Localization.Resources.AbpUi;
using Taitans.Abp.AuditLogging.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Taitans.Abp.AuditLogging
{
    [DependsOn(
        typeof(AbpAuditLoggingApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpAuditLoggingHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAuditLoggingHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AuditLoggingResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
