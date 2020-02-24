using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Taitans.Abp.OcelotManagement.EntityFrameworkCore
{
    [ConnectionStringName(AbpOcelotManagementDbProperties.ConnectionStringName)]
    public class OcelotManagementDbContext : AbpDbContext<OcelotManagementDbContext>, IOcelotManagementDbContext
    {
        public DbSet<Ocelot> GlobalConfigurations { get; set; }

        public OcelotManagementDbContext(DbContextOptions<OcelotManagementDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureOcelot();
        }
    }
}