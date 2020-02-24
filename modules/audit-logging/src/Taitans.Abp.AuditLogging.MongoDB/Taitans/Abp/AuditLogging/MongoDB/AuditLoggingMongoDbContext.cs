using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.AuditLogging.MongoDB
{
    [ConnectionStringName(AuditLoggingDbProperties.ConnectionStringName)]
    public class AuditLoggingMongoDbContext : AbpMongoDbContext, IAuditLoggingMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureAuditLogging();
        }
    }
}