using Mongo2Go;
using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Taitans.Abp.OcelotManagement.MongoDB
{
    [DependsOn(
        typeof(AbpOcelotManagementTestBaseModule),
        typeof(AbpOcelotManagementMongoDbModule)
        )]
    public class AbpOcelotManagementMongoDbTestModule : AbpModule
    {
        private static readonly MongoDbRunner MongoDbRunner = MongoDbRunner.Start();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbRunner.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}