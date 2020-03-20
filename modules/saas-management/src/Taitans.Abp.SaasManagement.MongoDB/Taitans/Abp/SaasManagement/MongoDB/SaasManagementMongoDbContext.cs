using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.SaasManagement.MongoDB
{
    [ConnectionStringName(AbpSaasManagementDbProperties.ConnectionStringName)]
    public class SaasManagementMongoDbContext : AbpMongoDbContext, ISaasManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureSaasManagement();
        }
    }
}