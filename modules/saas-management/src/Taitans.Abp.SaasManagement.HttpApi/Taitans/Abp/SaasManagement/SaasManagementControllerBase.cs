using Taitans.Abp.SaasManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Taitans.Abp.SaasManagement
{
    public abstract class SaasManagementControllerBase : AbpController
    {
        protected SaasManagementControllerBase()
        {
            LocalizationResource = typeof(AbpSaasManagementResource);
        }
    }
}
