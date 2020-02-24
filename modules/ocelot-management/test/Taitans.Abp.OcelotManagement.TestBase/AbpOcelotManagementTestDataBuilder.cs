using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Taitans.Abp.OcelotManagement
{
    public class AbpOcelotManagementTestDataBuilder : ITransientDependency
    {
        private readonly IOcelotRepository _ocelotRepository;

        public AbpOcelotManagementTestDataBuilder(
             IOcelotRepository ocelotRepository)
        {
            _ocelotRepository = ocelotRepository;
        }

        public void Build()
        {
            AsyncHelper.RunSync(AddOcelotsAsync);
        }

        public async Task AddOcelotsAsync()
        {
            var globalConfig = await _ocelotRepository.GetCountAsync();
            if (globalConfig <= 0)
            {
                var global = new Ocelot(
                    Guid.NewGuid(),
                    "middleware",
                    "gateway",
                    "http://localhost:53470",
                    "http");

                global.ServiceDiscoveryProvider = new OcelotServiceDiscoveryProvider
                {
                    Host = "192.168.88.88",
                    ConfigurationKey = "Taitans-NB",
                    PollingInterval = 8888,
                    Port = 2888,
                    Token = "Taitans-Token",
                    Type = "Taitans",
                    Namespace = "TaitansStudio"
                };

                global.QoSOption = new OcelotQoSOption
                {
                    DurationOfBreak = 2888,
                    ExceptionsAllowedBeforeBreaking = 8888,
                    TimeoutValue = 1888
                };

                global.RateLimitOption = new OcelotRateLimitOption
                {
                    DisableRateLimitHeaders = true,
                    ClientIdHeader = "Taitans-Studio",
                    HttpStatusCode = 200,
                    QuotaExceededMessage = "WO_CAO_YI_CHANG",
                    RateLimitCounterPrefix = "Taitans"
                };

                global.HttpHandlerOption = new OcelotHttpHandlerOption
                {
                    AllowAutoRedirect = true,
                    UseCookieContainer = true,
                    UseProxy = true,
                    UseTracing = true
                };

                global.LoadBalancerOption = new OcelotLoadBalancerOption
                {
                    Expiry = 2888,
                    Key = "Taitans",
                    Type = "Studio"
                };


                var host = new Dictionary<string, int>();
                host.Add("taitans.middleground.identityserver", 80);
                global.AddReRoutes(
                    "Token",
                    "/connect/token",
                    null,
                    "http",
                    "/connect/token",
                    new List<string>() { "POST" },
                    host
                );

                global.ReRoutes[0].Timeout = 4399;
                global.ReRoutes[0].Priority = 3389;
                global.ReRoutes[0].AddDelegatingHandler("Taitans");
                global.ReRoutes[0].Key = "WO_CAO";
                global.ReRoutes[0].UpstreamHost = "http://www.taitans.com";
                global.ReRoutes[0].HttpHandlerOption = new ReRouteHttpHandlerOption
                {
                    AllowAutoRedirect = true,
                    UseCookieContainer = true,
                    UseProxy = true,
                    UseTracing = true,
                };
                global.ReRoutes[0].AuthenticationOption = new ReRouteAuthenticationOption
                {
                    AuthenticationProviderKey = "Taitans",
                };
                global.ReRoutes[0].AuthenticationOption.AddScope("Taitans");

                global.ReRoutes[0].RateLimitOption = new ReRouteRateLimitRule
                {
                    Period = "Tatains",
                    PeriodTimespan = 0.1415926,
                    EnableRateLimiting = true,
                    Limit = 5201314,




                };
                global.ReRoutes[0].RateLimitOption.AddWhitelist("Taitans");

                global.ReRoutes[0].LoadBalancerOption = new ReRouteLoadBalancerOption
                {
                    Key = "Taitans",
                    Type = "www.taitans.com",
                    Expiry = 95
                };

                global.ReRoutes[0].QoSOption = new ReRouteQoSOption
                {
                    ExceptionsAllowedBeforeBreaking = 802,
                    DurationOfBreak = 24300,
                    TimeoutValue = 30624
                };

                global.ReRoutes[0].ServiceName = "taitans-cn";
                global.ReRoutes[0].ReRouteIsCaseSensitive = true;
                global.ReRoutes[0].CacheOption = new ReRouteCacheOption
                {
                    TtlSeconds = 2020,
                    Region = "github.com/taitans"
                };


                global.ReRoutes[0].RequestIdKey = "ttgzs.cn";
                global.ReRoutes[0].AddQueriesToRequests.Add("NB", "www.taitans.com");
                global.ReRoutes[0].RouteClaimsRequirements.Add("MVP", "www.taitans.com");
                global.ReRoutes[0].AddClaimsToRequests.Add("AT", "www.taitans.com");
                global.ReRoutes[0].DownstreamHeaderTransforms.Add("CVT", "www.taitans.com");
                global.ReRoutes[0].UpstreamHeaderTransforms.Add("DCT", "www.taitans.com");
                global.ReRoutes[0].AddHeadersToRequests.Add("Trubost", "www.taitans.com");
                global.ReRoutes[0].ChangeDownstreamPathTemplates.Add("EVCT", "www.taitans.com");

                global.ReRoutes[0].DangerousAcceptAnyServerCertificateValidator = true;

                global.ReRoutes[0].SecurityOption = new ReRouteSecurityOption();
                global.ReRoutes[0].SecurityOption.AddIPAllowed("199.88.88.88");
                global.ReRoutes[0].SecurityOption.AddIPBlocked("88.88.88.88");

                var host2 = new Dictionary<string, int>();
                host2.Add("taitans.middleground.httpapi.host", 80);
                global.AddReRoutes(
                   "AllUrl",
                   "/{url}",
                   null,
                   "http",
                   "/{url}",
                   new List<string>() { "GET", "POST", "PUT", "DELETE", "OPTIONS" },
                   host2
               );
                await _ocelotRepository.InsertAsync(global);
            }
        }
    }
}