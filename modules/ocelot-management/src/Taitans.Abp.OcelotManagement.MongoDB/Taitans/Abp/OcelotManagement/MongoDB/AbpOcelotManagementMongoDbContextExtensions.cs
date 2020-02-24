using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.OcelotManagement.MongoDB
{
    public static class AbpOcelotManagementMongoDbContextExtensions
    {
        public static void ConfigureOcelotManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OcelotManagementMongoModelBuilderConfigurationOptions(
                AbpOcelotManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<Ocelot>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Ocelots";

            });
        }
    }
}