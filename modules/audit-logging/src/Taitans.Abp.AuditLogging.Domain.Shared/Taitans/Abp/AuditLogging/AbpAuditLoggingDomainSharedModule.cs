using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Taitans.Abp.AuditLogging.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AuditLogging;

namespace Taitans.Abp.AuditLogging
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(Volo.Abp.AuditLogging.AbpAuditLoggingDomainSharedModule)
    )]
    public class AbpAuditLoggingDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAuditLoggingDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AuditLoggingResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Taitans/Abp/AuditLogging/Localization/AuditLogging");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("AuditLogging", typeof(AuditLoggingResource));
            });
        }
    }
}
