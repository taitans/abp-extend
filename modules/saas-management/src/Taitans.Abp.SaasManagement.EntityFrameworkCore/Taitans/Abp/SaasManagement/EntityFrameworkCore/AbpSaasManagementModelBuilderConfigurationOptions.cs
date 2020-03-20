using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    public class AbpSaasManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public AbpSaasManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}