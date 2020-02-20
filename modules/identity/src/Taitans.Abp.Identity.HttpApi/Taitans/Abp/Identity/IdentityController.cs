using Taitans.Abp.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Taitans.Abp.Identity
{
    public abstract class IdentityController : AbpController
    {
        protected IdentityController()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
