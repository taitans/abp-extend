using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;

namespace Taitans.Abp.AuditLogging
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(Volo.Abp.AuditLogging.AbpAuditLoggingDomainModule)
    )]
    public class AbpAuditLoggingDomainModule : AbpModule
    {

    }
}
