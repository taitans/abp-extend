using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.SaasManagement.MongoDB
{
    public static class AbpSaasManagementMongoDbContextExtensions
    {
        public static void ConfigureSaasManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpSaasManagementMongoModelBuilderConfigurationOptions(
                AbpSaasManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}