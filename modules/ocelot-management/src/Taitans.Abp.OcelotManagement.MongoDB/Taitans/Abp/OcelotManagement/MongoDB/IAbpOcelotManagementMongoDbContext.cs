using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.OcelotManagement.MongoDB
{
    [ConnectionStringName(AbpOcelotManagementDbProperties.ConnectionStringName)]
    public interface IAbpOcelotManagementMongoDbContext : IAbpMongoDbContext
    {
        IMongoCollection<Ocelot> Ocelots { get; }
    }
}
