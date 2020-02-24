using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.AuditLogging.MongoDB
{
    public static class AuditLoggingMongoDbContextExtensions
    {
        public static void ConfigureAuditLogging(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AuditLoggingMongoModelBuilderConfigurationOptions(
                AuditLoggingDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}