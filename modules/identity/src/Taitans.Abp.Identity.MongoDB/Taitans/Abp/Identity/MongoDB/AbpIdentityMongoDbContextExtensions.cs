using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.Identity.MongoDB
{
    public static class AbpIdentityMongoDbContextExtensions
    {
        public static void ConfigureIdentity(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new IdentityMongoModelBuilderConfigurationOptions(
                AbpIdentityDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}