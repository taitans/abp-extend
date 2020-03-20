using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpSaasManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class AbpSaasManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SaasManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}