using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    public static class AbpSaasManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureSaasManagement(
            this ModelBuilder builder,
            Action<AbpSaasManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpSaasManagementModelBuilderConfigurationOptions(
                AbpSaasManagementDbProperties.DbTablePrefix,
                AbpSaasManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Edition>(b =>
            {
                b.ToTable(options.TablePrefix + "Editions", options.Schema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(t => t.DisplayName).HasMaxLength(EditionConsts.MaxNameLength);

                b.HasIndex(c => c.DisplayName);
            });

            builder.Entity<Tenant>(b =>
            {
                b.ToTable(options.TablePrefix + "Tenants", options.Schema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(t => t.Name).IsRequired().HasMaxLength(TenantConsts.MaxNameLength);

                b.HasMany(u => u.ConnectionStrings).WithOne().HasForeignKey(uc => uc.TenantId).IsRequired();

                b.HasOne(u => u.Edition).WithMany().HasForeignKey(uc => uc.EditionId);

                b.Ignore(u => u.EditionName);

                b.HasIndex(u => u.Name);
            });

            builder.Entity<TenantConnectionString>(b =>
            {
                b.ToTable(options.TablePrefix + "TenantConnectionStrings", options.Schema);

                b.HasKey(x => new { x.TenantId, x.Name });

                b.Property(cs => cs.Name).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxNameLength);
                b.Property(cs => cs.Value).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxValueLength);
            });
        }
    }
}