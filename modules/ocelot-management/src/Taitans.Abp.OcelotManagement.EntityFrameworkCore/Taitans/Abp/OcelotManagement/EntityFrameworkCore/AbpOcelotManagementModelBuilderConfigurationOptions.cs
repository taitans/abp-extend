using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Taitans.Abp.OcelotManagement.EntityFrameworkCore
{
    public class AbpOcelotManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public AbpOcelotManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}