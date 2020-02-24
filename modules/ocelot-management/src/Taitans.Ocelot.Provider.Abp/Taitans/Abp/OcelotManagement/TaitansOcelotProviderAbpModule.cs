using Taitans.Abp.OcelotManagement.EntityFrameworkCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Taitans.Ocelot.Provider.Abp
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpOcelotEntityFrameworkCoreModule)
    )]
    public class TaitansOcelotProviderAbpModule : AbpModule
    {
    }
}
