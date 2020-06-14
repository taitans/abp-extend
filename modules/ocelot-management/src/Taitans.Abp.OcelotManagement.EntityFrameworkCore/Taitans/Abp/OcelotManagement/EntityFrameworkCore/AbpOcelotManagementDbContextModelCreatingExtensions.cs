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
        public static void ConfigureOcelotManagement(
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

            builder.Entity<RouteDelegatingHandler>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteDelegatingHandler",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.Delegating });
            });

            builder.Entity<RouteDownstreamHostAndPort>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteDownstreamHostAndPorts",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.Host, r.Port });
            });

            builder.Entity<RouteHttpHandlerOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteHttpHandlerOptions",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });
            });

            builder.Entity<RouteAuthenticationOptionAllowedScope>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteAuthenticationOptionAllowedScope",
                    options.Schema);

                b.Property(t => t.Scope).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.Scope });
            });

            builder.Entity<RouteAuthenticationOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteAuthenticationOptions",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });

                b.HasMany(r => r.AllowedScopes).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);
            });



            builder.Entity<RouteRateLimitRule>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteRateLimitRules",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });

                b.HasMany(r => r.ClientWhitelist).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<RouteRateLimitRuleClientWhitelist>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteRateLimitRuleClientWhitelist",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.Whitelist });
            });

            builder.Entity<RouteLoadBalancerOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteLoadBalancerOptions",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });
            });

            builder.Entity<RouteQoSOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteQoSOptions",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });
            });

            builder.Entity<RouteCacheOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteCacheOptions",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });
            });

            builder.Entity<RouteUpstreamHttpMethod>(b =>
            {
                b.ToTable(AbpOcelotManagementDbProperties.DbTablePrefix + "RouteUpstreamHttpMethods",
                   AbpOcelotManagementDbProperties.DbSchema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.Method });
            });

            builder.Entity<RouteSecurityOption>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteSecurityOptions",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName });

                b.HasMany(r => r.IPAllowedList).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasMany(r => r.IPBlockedList).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<RouteSecurityOptionIPAllowed>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteSecurityOptionIPAllowed",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.IP });

            });

            builder.Entity<RouteSecurityOptionIPBlocked>(b =>
            {
                b.ToTable(options.TablePrefix + "RouteSecurityOptionIPBlocked",
                    options.Schema);

                b.Property(t => t.RouteName).HasMaxLength(OcelotConsts.MaxNameLength);

                b.HasKey(r => new { r.GlobalConfigurationId, r.RouteName, r.IP });
            });




            builder.Entity<OcelotRoute>(b =>
            {
                b.ToTable(AbpOcelotManagementDbProperties.DbTablePrefix + "Routes",
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

                b.HasMany(c => c.DelegatingHandlers).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);
                b.HasMany(c => c.DownstreamHostAndPorts).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade).IsRequired();

                b.HasOne(c => c.HttpHandlerOption).WithOne().HasForeignKey<RouteHttpHandlerOption>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.AuthenticationOption).WithOne().HasForeignKey<RouteAuthenticationOption>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.RateLimitOption).WithOne().HasForeignKey<RouteRateLimitRule>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.LoadBalancerOption).WithOne().HasForeignKey<RouteLoadBalancerOption>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.QoSOption).WithOne().HasForeignKey<RouteQoSOption>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasOne(c => c.CacheOption).WithOne().HasForeignKey<RouteCacheOption>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);

                b.HasMany(c => c.UpstreamHttpMethods).WithOne().HasForeignKey(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade).IsRequired();

                b.HasOne(c => c.SecurityOption).WithOne().HasForeignKey<RouteSecurityOption>(r => new { r.GlobalConfigurationId, r.RouteName }).OnDelete(DeleteBehavior.Cascade);
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

                b.HasMany(c => c.Routes).WithOne().HasForeignKey(c => c.GlobalConfigurationId).OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(c => c.Name); //设置唯一
            });
        }
    }
}