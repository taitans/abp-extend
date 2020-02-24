using Taitans.Abp.AuditLogging.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Taitans.Abp.AuditLogging
{
    public abstract class AuditLoggingControllerBase : AbpController
    {
        protected AuditLoggingControllerBase()
        {
            LocalizationResource = typeof(AuditLoggingResource);
        }
    }
}
