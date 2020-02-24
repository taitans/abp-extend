using Taitans.Abp.OcelotManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotController : AbpController
    {
        protected OcelotController()
        {
            LocalizationResource = typeof(AbpOcelotManagementResource);
        }
    }
}
