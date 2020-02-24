


using Taitans.Abp.AuditLogging.Localization;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.AuditLogging
{
    public abstract class AuditLoggingAppServiceBase : ApplicationService
    {
        protected AuditLoggingAppServiceBase()
        {
            LocalizationResource = typeof(AuditLoggingResource);
            ObjectMapperContext = typeof(AbpAuditLoggingApplicationModule);
        }
    }
}
