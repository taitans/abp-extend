using Taitans.Abp.SaasManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Taitans.Abp.SaasManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(AbpSaasManagementEntityFrameworkCoreTestModule)
        )]
    public class AbpSaasManagementDomainTestModule : AbpModule
    {
        
    }
}
