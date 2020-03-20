using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.SaasManagement.MongoDB
{
    [ConnectionStringName(AbpSaasManagementDbProperties.ConnectionStringName)]
    public interface ISaasManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
