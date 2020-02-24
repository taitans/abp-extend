using Taitans.Abp.OcelotManagement.Localization;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotManagementAppServiceBase : ApplicationService
    {
        protected OcelotManagementAppServiceBase()
        {
            LocalizationResource = typeof(AbpOcelotManagementResource);
            ObjectMapperContext = typeof(AbpOcelotManagementApplicationModule);
        }
    }
}
