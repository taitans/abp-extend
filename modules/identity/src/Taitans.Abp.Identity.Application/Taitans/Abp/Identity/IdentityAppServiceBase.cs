using Taitans.Abp.Identity.Localization;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.Identity
{
    public abstract class IdentityAppServiceBase : ApplicationService
    {
        protected IdentityAppServiceBase()
        {
            LocalizationResource = typeof(IdentityResource);
            ObjectMapperContext = typeof(AbpIdentityApplicationModule);
        }
    }
}
