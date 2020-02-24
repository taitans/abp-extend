using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.EntityFrameworkCore.ValueComparers;
using Volo.Abp.EntityFrameworkCore.ValueConverters;

namespace Taitans.Abp.OcelotManagement.EntityFrameworkCore
{
    public static class AbpOcelotManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureOcelot(
            this ModelBuilder builder,
            Action<AbpOcelotManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpOcelotManagementModelBuilderConfigurationOptions(
                AbpOcelotManagementDbProperties.DbTablePrefix,
                AbpOcelotManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            Check.NotNull(builder, nameof(builder));

            builder.Entity<ReRouteDelegatingHandler>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteDelegatingHandler",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.Delegating });
            });

            builder.Entity<ReRouteDownstreamHostAndPort>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteDownstreamHostAndPorts",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.Host, r.Port });
            });

            builder.Entity<ReRouteHttpHandlerOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteHttpHandlerOptions",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });
            });

            builder.Entity<ReRouteAuthenticationOptionAllowedScope>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteAuthenticationOptionAllowedScope",
                    options.Schema);

                b.Property(t => t.Scope).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.Scope });
            });

            builder.Entity<ReRouteAuthenticationOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteAuthenticationOptions",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });

                b.HasMany(r => r.AllowedScopes).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);
            });



            builder.Entity<ReRouteRateLimitRule>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteRateLimitRules",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });

                b.HasMany(r => r.ClientWhitelist).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ReRouteRateLimitRuleClientWhitelist>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteRateLimitRuleClientWhitelist",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.Whitelist });
            });

            builder.Entity<ReRouteLoadBalancerOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteLoadBalancerOptions",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });
            });

            builder.Entity<ReRouteQoSOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteQoSOptions",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });
            });

            builder.Entity<ReRouteCacheOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteCacheOptions",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });
            });

            builder.Entity<ReRouteUpstreamHttpMethod>(b =>
            {
                b.ToTable(AbpOcelotManagementDbProperties.DbTablePrefix + "ReRouteUpstreamHttpMethods",
                   AbpOcelotManagementDbProperties.DbSchema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.Method });
            });

            builder.Entity<ReRouteSecurityOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteSecurityOptions",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName });

                b.HasMany(r => r.IPAllowedList).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasMany(r => r.IPBlockedList).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ReRouteSecurityOptionIPAllowed>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteSecurityOptionIPAllowed",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.IP });

            });

            builder.Entity<ReRouteSecurityOptionIPBlocked>(b =>
            {
                b.ToTable(options.TablePrefix + "ReRouteSecurityOptionIPBlocked",
                    options.Schema);

                b.Property(t => t.ReRouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.ReRouteName, r.IP });
            });




            builder.Entity<OcelotReRoute>(b =>
            {
                b.ToTable(AbpOcelotManagementDbProperties.DbTablePrefix + "ReRoutes",
                    AbpOcelotManagementDbProperties.DbSchema);

                b.Property(t => t.Name).IsRequired().HasMaxLength(OcelotConsts.MaxNameLength);

                b.Property(t => t.AddQueriesToRequests)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.Property(t => t.RouteClaimsRequirements)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.Property(t => t.AddClaimsToRequests)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.Property(t => t.DownstreamHeaderTransforms)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.Property(t => t.UpstreamHeaderTransforms)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.Property(t => t.AddHeadersToRequests)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.Property(t => t.ChangeDownstreamPathTemplates)
                    .HasConversion(new AbpJsonValueConverter<Dictionary<string, string>>())
                    .Metadata.SetValueComparer(new AbpDictionaryValueComparer<string, string>());

                b.HasKey(x => new { x.GlobalConfigurationId, x.Name });

                b.HasMany(c => c.DelegatingHandlers).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);
                b.HasMany(c => c.DownstreamHostAndPorts).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade).IsRequired();

                b.HasOne(c => c.HttpHandlerOption).WithOne().HasForeignKey<ReRouteHttpHandlerOption>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.AuthenticationOption).WithOne().HasForeignKey<ReRouteAuthenticationOption>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.RateLimitOption).WithOne().HasForeignKey<ReRouteRateLimitRule>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.LoadBalancerOption).WithOne().HasForeignKey<ReRouteLoadBalancerOption>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.QoSOption).WithOne().HasForeignKey<ReRouteQoSOption>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.CacheOption).WithOne().HasForeignKey<ReRouteCacheOption>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasMany(c => c.UpstreamHttpMethods).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade).IsRequired();

                b.HasOne(c => c.SecurityOption).WithOne().HasForeignKey<ReRouteSecurityOption>(r => new { r.GlobalConfigurationId, r.ReRouteName }).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<OcelotServiceDiscoveryProvider>(b =>
            {
                b.ToTable(options.TablePrefix + "ServiceDiscoveryProviders",
                   options.Schema);
                b.HasKey(r => new { r.GlobalConfigurationId });
            });

            builder.Entity<OcelotRateLimitOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RateLimitOptions",
                   options.Schema);
                b.HasKey(r => new { r.GlobalConfigurationId });
            });

            builder.Entity<OcelotQoSOption>(b =>
            {
                b.ToTable(options.TablePrefix + "QoSOptions",
                   options.Schema);
                b.HasKey(r => new { r.GlobalConfigurationId });
            });

            builder.Entity<OcelotLoadBalancerOption>(b =>
            {
                b.ToTable(options.TablePrefix + "LoadBalancerOptions",
                   options.Schema);
                b.HasKey(r => new { r.GlobalConfigurationId });
            });


            builder.Entity<OcelotHttpHandlerOption>(b =>
            {
                b.ToTable(options.TablePrefix + "HttpHandlerOptions",
                    options.Schema);
                b.HasKey(r => new { r.GlobalConfigurationId });
            });


            builder.Entity<Ocelot>(b =>
            {
                b.ToTable("Ocelots",
                         options.Schema);
                b.ConfigureFullAuditedAggregateRoot();

                b.Property(t => t.Name).IsRequired().HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasOne(c => c.ServiceDiscoveryProvider).WithOne().HasForeignKey<OcelotServiceDiscoveryProvider>(r => new { r.GlobalConfigurationId }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.RateLimitOption).WithOne().HasForeignKey<OcelotRateLimitOption>(r => new { r.GlobalConfigurationId }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.QoSOption).WithOne().HasForeignKey<OcelotQoSOption>(r => new { r.GlobalConfigurationId }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.LoadBalancerOption).WithOne().HasForeignKey<OcelotLoadBalancerOption>(r => new { r.GlobalConfigurationId }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.HttpHandlerOption).WithOne().HasForeignKey<OcelotHttpHandlerOption>(r => new { r.GlobalConfigurationId }).OnDelete(DeleteBehavior.Cascade);

                b.HasMany(c => c.ReRoutes).WithOne().HasForeignKey(c => c.GlobalConfigurationId).OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(c => c.Name); //设置唯一
            });
        }
    }
}