using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Taitans.Abp.OcelotManagement.EntityFrameworkCore
{
    [ConnectionStringName(AbpOcelotManagementDbProperties.ConnectionStringName)]
    public interface IOcelotManagementDbContext : IEfCoreDbContext
    {
        DbSet<Ocelot> GlobalConfigurations { get; set; }
    }
}