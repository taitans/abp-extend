using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.SaasManagement.MongoDB
{
    public class AbpSaasManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public AbpSaasManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}