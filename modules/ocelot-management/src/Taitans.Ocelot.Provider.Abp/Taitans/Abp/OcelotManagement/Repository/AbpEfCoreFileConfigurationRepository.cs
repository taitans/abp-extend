using Newtonsoft.Json;
using Ocelot.Cache;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;
using Ocelot.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taitans.Abp.OcelotManagement;
using Taitans.Ocelot.Provider.Abp.Configuration;

namespace Taitans.Ocelot.Provider.Abp.Repository
{
    public class AbpEfCoreFileConfigurationRepository : IFileConfigurationRepository
    {
        private readonly IOcelotRepository _ocelotGlobalConfigurationRepository;
        private readonly ConfigCacheOptions _option;
        private readonly IOcelotCache<FileConfiguration> _cache;
        private readonly IOcelotLogger _logger;

        public AbpEfCoreFileConfigurationRepository(ConfigCacheOptions option, IOcelotCache<FileConfiguration> cache, IOcelotRepository ocelotGlobalConfigurationRepository, IOcelotLoggerFactory loggerFactory)
        {
            _ocelotGlobalConfigurationRepository = ocelotGlobalConfigurationRepository;
            _option = option;
            _cache = cache;
            _logger = loggerFactory.CreateLogger<AbpEfCoreFileConfigurationRepository>();
        }
        public async Task<Response<FileConfiguration>> Get()
        {
            var config = _cache.Get(_option.CachePrefix + "FileConfiguration", "");

            if (config != null)
            {
                return new OkResponse<FileConfiguration>(config);
            }

            var file = new FileConfiguration();

            // 提取全局配置信息
            var globalResult = await _ocelotGlobalConfigurationRepository.FindByNameAsync(_option.GatewayName);

            if (globalResult != null)
            {
                var fileGlobalConfig = new FileGlobalConfiguration
                {
                    BaseUrl = globalResult.BaseUrl,
                    DownstreamScheme = globalResult.DownstreamScheme,
                    RequestIdKey = globalResult.RequestIdKey
                };

                if (globalResult.HttpHandlerOption != null)
                {
                    var httpHandlerOption = globalResult.HttpHandlerOption;
                    fileGlobalConfig.HttpHandlerOptions = new FileHttpHandlerOptions
                    {
                        AllowAutoRedirect = httpHandlerOption.AllowAutoRedirect,
                        UseCookieContainer = httpHandlerOption.UseCookieContainer,
                        UseProxy = httpHandlerOption.UseProxy,
                        UseTracing = httpHandlerOption.UseTracing,
                        MaxConnectionsPerServer = httpHandlerOption.MaxConnectionsPerServer
                    };
                }
                if (globalResult.LoadBalancerOption != null)
                {
                    var loadBalancerOption = globalResult.LoadBalancerOption;
                    fileGlobalConfig.LoadBalancerOptions = new FileLoadBalancerOptions
                    {
                        Expiry = loadBalancerOption.Expiry,
                        Key = loadBalancerOption.Key,
                        Type = loadBalancerOption.Type
                    };
                }
                if (globalResult.QoSOption != null)
                {
                    var qoSOption = globalResult.QoSOption;
                    fileGlobalConfig.QoSOptions = new FileQoSOptions
                    {
                        ExceptionsAllowedBeforeBreaking = qoSOption.ExceptionsAllowedBeforeBreaking,
                        DurationOfBreak = qoSOption.DurationOfBreak,
                        TimeoutValue = qoSOption.TimeoutValue
                    };
                }
                if (globalResult.ServiceDiscoveryProvider != null)
                {
                    var serviceDiscoveryProvider = globalResult.ServiceDiscoveryProvider;
                    fileGlobalConfig.ServiceDiscoveryProvider = new FileServiceDiscoveryProvider
                    {
                        ConfigurationKey = serviceDiscoveryProvider.ConfigurationKey,
                        Host = serviceDiscoveryProvider.Host,
                        Namespace = serviceDiscoveryProvider.Namespace,
                        PollingInterval = serviceDiscoveryProvider.PollingInterval,
                        Port = serviceDiscoveryProvider.Port,
                        Token = serviceDiscoveryProvider.Token,
                        Type = serviceDiscoveryProvider.Type
                    };
                }
                if (globalResult.RateLimitOption != null)
                {
                    var rateLimitOption = globalResult.RateLimitOption;
                    fileGlobalConfig.RateLimitOptions = new FileRateLimitOptions
                    {
                        ClientIdHeader = rateLimitOption.ClientIdHeader,
                        DisableRateLimitHeaders = rateLimitOption.DisableRateLimitHeaders,
                        HttpStatusCode = rateLimitOption.HttpStatusCode,
                        QuotaExceededMessage = rateLimitOption.QuotaExceededMessage,
                        RateLimitCounterPrefix = rateLimitOption.RateLimitCounterPrefix
                    };
                }

                file.GlobalConfiguration = fileGlobalConfig;
                //TODO: Optimize code structure
                if (globalResult?.ReRoutes?.Count > 0)
                {
                    _logger.LogInformation($"ReRoute Count:{ globalResult?.ReRoutes?.Count }");
                    //提取路由信息
                    var reRoutes = globalResult.ReRoutes.OrderBy(c => c.Sort);

                    List<FileReRoute> fileReRoutes = new List<FileReRoute>();
                    foreach (var route in reRoutes)
                    {
                        _logger.LogInformation($"Loading ReRoute: {route.Name}");
                        var r = new FileReRoute
                        {
                            Key = route.Key,
                            Priority = route.Priority,
                            ServiceName = route.ServiceName,
                            Timeout = route.Timeout,
                            DownstreamPathTemplate = route.DownstreamPathTemplate,
                            DownstreamScheme = route.DownstreamScheme,
                            UpstreamHost = route.UpstreamHost,
                            DangerousAcceptAnyServerCertificateValidator = route.DangerousAcceptAnyServerCertificateValidator,
                            DownstreamHttpMethod = route.DownstreamHttpMethod,
                            RequestIdKey = route.RequestIdKey,
                            UpstreamPathTemplate = route.UpstreamPathTemplate,
                            ServiceNamespace = route.ServiceNamespace,
                            ReRouteIsCaseSensitive = route.ReRouteIsCaseSensitive,
                            AddClaimsToRequest = route.AddClaimsToRequests,
                            AddHeadersToRequest = route.AddHeadersToRequests,
                            AddQueriesToRequest = route.AddQueriesToRequests,
                            ChangeDownstreamPathTemplate = route.ChangeDownstreamPathTemplates,
                            DownstreamHeaderTransform = route.DownstreamHeaderTransforms,
                            RouteClaimsRequirement = route.RouteClaimsRequirements,
                            UpstreamHeaderTransform = route.UpstreamHeaderTransforms,
                            // AuthenticationOptions = null,
                            // DelegatingHandlers = null,
                            // DownstreamHostAndPorts = null,
                            // FileCacheOptions = null,
                            // HttpHandlerOptions = null,
                            // LoadBalancerOptions = null,
                            // QoSOptions = null,
                            // RateLimitOptions = null,
                            // SecurityOptions = null,
                            // UpstreamHttpMethod = null
                        };
                        if (route.AuthenticationOption != null)
                        {
                            var authenticationOption = route.AuthenticationOption;
                            r.AuthenticationOptions = new FileAuthenticationOptions
                            {
                                AuthenticationProviderKey = authenticationOption.AuthenticationProviderKey,
                                AllowedScopes = authenticationOption.AllowedScopes.Select(c => c.Scope).ToList()
                            };
                        }
                        if (route.DelegatingHandlers != null && route.DelegatingHandlers.Count > 0)
                        {
                            r.DelegatingHandlers = route.DelegatingHandlers.Select(c => c.Delegating).ToList();
                        }
                        if (route.DownstreamHostAndPorts != null && route.DownstreamHostAndPorts.Count > 0)
                        {
                            var downstreamHostAndPorts = new List<FileHostAndPort>();
                            foreach (var host in route.DownstreamHostAndPorts)
                            {
                                downstreamHostAndPorts.Add(new FileHostAndPort
                                {
                                    Host = host.Host,
                                    Port = host.Port
                                });
                            }
                            r.DownstreamHostAndPorts = downstreamHostAndPorts;
                        }
                        if (route.CacheOption != null)
                        {
                            var cacheOption = route.CacheOption;
                            r.FileCacheOptions = new FileCacheOptions
                            {
                                TtlSeconds = cacheOption.TtlSeconds,
                                Region = cacheOption.Region
                            };
                        }
                        if (route.HttpHandlerOption != null)
                        {
                            var httpHandlerOption = route.HttpHandlerOption;
                            r.HttpHandlerOptions = new FileHttpHandlerOptions
                            {
                                AllowAutoRedirect = httpHandlerOption.AllowAutoRedirect,
                                UseCookieContainer = httpHandlerOption.UseCookieContainer,
                                UseProxy = httpHandlerOption.UseProxy,
                                UseTracing = httpHandlerOption.UseTracing,
                                MaxConnectionsPerServer = httpHandlerOption.MaxConnectionsPerServer
                            };
                        }
                        if (route.LoadBalancerOption != null)
                        {
                            var loadBalancerOptions = route.LoadBalancerOption;
                            r.LoadBalancerOptions = new FileLoadBalancerOptions
                            {
                                Expiry = loadBalancerOptions.Expiry,
                                Key = loadBalancerOptions.Key,
                                Type = loadBalancerOptions.Type
                            };
                        }
                        if (route.QoSOption != null)
                        {
                            var qoSOption = route.QoSOption;
                            r.QoSOptions = new FileQoSOptions
                            {
                                ExceptionsAllowedBeforeBreaking = qoSOption.ExceptionsAllowedBeforeBreaking,
                                DurationOfBreak = qoSOption.DurationOfBreak,
                                TimeoutValue = qoSOption.TimeoutValue
                            };
                        }
                        if (route.RateLimitOption != null)
                        {
                            var rateLimitOption = route.RateLimitOption;
                            r.RateLimitOptions = new FileRateLimitRule
                            {
                                ClientWhitelist = rateLimitOption.ClientWhitelist.Select(c => c.Whitelist).ToList(),
                                EnableRateLimiting = rateLimitOption.EnableRateLimiting,
                                Limit = rateLimitOption.Limit,
                                Period = rateLimitOption.Period,
                                PeriodTimespan = rateLimitOption.PeriodTimespan
                            };
                        }
                        if (route.SecurityOption != null)
                        {
                            var securityOption = route.SecurityOption;
                            r.SecurityOptions = new FileSecurityOptions
                            {
                                IPAllowedList = securityOption.IPAllowedList.Select(c => c.IP).ToList(),
                                IPBlockedList = securityOption.IPBlockedList.Select(c => c.IP).ToList()
                            };
                        }

                        if (route.UpstreamHttpMethods != null)
                        {
                            r.UpstreamHttpMethod = route.UpstreamHttpMethods.Select(c => c.Method).ToList();
                        }
                        r.UpstreamPathTemplate = route.UpstreamPathTemplate;

                        file.ReRoutes.Add(r);
                    }
                }
                else
                {
                    _logger.LogWarning($"Not Found ReRoute");
                }
            }
            else
            {
                throw new Exception(string.Format("Not found '{0}' gateway name config", _option.GatewayName));
            }
            if (file.ReRoutes == null || file.ReRoutes.Count == 0)
            {
                return new OkResponse<FileConfiguration>(null);
            }
            _logger.LogDebug(JsonConvert.SerializeObject(file));

            return new OkResponse<FileConfiguration>(file);
        }

        public Task<Response> Set(FileConfiguration fileConfiguration)
        {
            _cache.AddAndDelete(_option.CachePrefix + "FileConfiguration", fileConfiguration, TimeSpan.FromSeconds(1800), "");
            return Task.FromResult((Response)new OkResponse());
        }
    }
}
