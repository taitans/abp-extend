using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Taitans.Abp.SaasManagement
{
    [DependsOn(
        typeof(AbpMultiTenancyModule),
        typeof(AbpSaasManagementDomainSharedModule),
        typeof(AbpDataModule),
        typeof(AbpDddDomainModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpSaasManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpSaasManagementDomainModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpSaasManagementDomainMappingProfile>(validate: true);
            });

            //context.Services.Configure<AbpDataSeedOptions>(options =>
            //{
            //    var adc = options.Contributors;
            //});
        }
    }
}
