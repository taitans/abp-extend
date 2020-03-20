using Volo.Abp.Modularity;

namespace Taitans.Abp.AuditLogging
{
    [DependsOn(
        typeof(AbpAuditLoggingApplicationModule),
        typeof(AuditLoggingDomainTestModule)
        )]
    public class AuditLoggingApplicationTestModule : AbpModule
    {

    }
}
