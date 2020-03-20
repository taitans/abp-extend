using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    [ConnectionStringName(AbpSaasManagementDbProperties.ConnectionStringName)]
    public interface ISaasManagementDbContext : IEfCoreDbContext
    {
        DbSet<Tenant> Tenants { get; set; } 
        DbSet<Edition> Editions { get; set; } 
    }
}