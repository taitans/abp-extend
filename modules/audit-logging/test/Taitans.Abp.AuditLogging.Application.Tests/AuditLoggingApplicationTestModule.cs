using Volo.Abp.Modularity;

namespace Taitans.Abp.AuditLogging
{
    [DependsOn(
        typeof(AuditLoggingApplicationModule),
        typeof(AuditLoggingDomainTestModule)
        )]
    public class AuditLoggingApplicationTestModule : AbpModule
    {

    }
}
