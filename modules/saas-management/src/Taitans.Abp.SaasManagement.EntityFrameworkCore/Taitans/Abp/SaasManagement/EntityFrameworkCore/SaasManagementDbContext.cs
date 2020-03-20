using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    [ConnectionStringName(AbpSaasManagementDbProperties.ConnectionStringName)]
    public class SaasManagementDbContext : AbpDbContext<SaasManagementDbContext>, ISaasManagementDbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Edition> Editions { get; set; }

        public SaasManagementDbContext(DbContextOptions<SaasManagementDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSaasManagement();
        }
    }
}