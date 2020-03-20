using Taitans.Abp.SaasManagement.Localization;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.SaasManagement
{
    public abstract class SaasManagementAppServiceBase : ApplicationService
    {
        protected SaasManagementAppServiceBase()
        {
            LocalizationResource = typeof(AbpSaasManagementResource);
            ObjectMapperContext = typeof(AbpSaasManagementApplicationModule);
        }
    }
}
