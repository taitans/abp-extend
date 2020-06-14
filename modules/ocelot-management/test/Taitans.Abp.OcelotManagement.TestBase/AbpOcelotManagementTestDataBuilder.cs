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

                global.ServiceDiscoveryProvider = new OcelotServiceDiscoveryProvider(global.Id)
                {
                    Host = "192.168.88.88",
                    ConfigurationKey = "Taitans-NB",
                    PollingInterval = 8888,
                    Port = 2888,
                    Token = "Taitans-Token",
                    Type = "Taitans",
                    Namespace = "TaitansStudio"
                };

                global.QoSOption = new OcelotQoSOption(global.Id)
                {
                    DurationOfBreak = 2888,
                    ExceptionsAllowedBeforeBreaking = 8888,
                    TimeoutValue = 1888
                };

                global.RateLimitOption = new OcelotRateLimitOption(global.Id)
                {
                    DisableRateLimitHeaders = true,
                    ClientIdHeader = "Taitans-Studio",
                    HttpStatusCode = 200,
                    QuotaExceededMessage = "WO_CAO_YI_CHANG",
                    RateLimitCounterPrefix = "Taitans"
                };

                global.HttpHandlerOption = new OcelotHttpHandlerOption(global.Id)
                {
                    AllowAutoRedirect = true,
                    UseCookieContainer = true,
                    UseProxy = true,
                    UseTracing = true
                };

                global.LoadBalancerOption = new OcelotLoadBalancerOption(global.Id)
                {
                    Expiry = 2888,
                    Key = "Taitans",
                    Type = "Studio"
                };


                var host = new Dictionary<string, int>();
                host.Add("Taitans.middleground.identityserver", 80);
                global.AddRoutes(
                    "Token",
                    "/connect/token",
                    null,
                    "GET",
                    "/connect/token",
                    "http",
                    upstreamHttpMethods: new List<string>() { "POST" },
                    downstreamHostAndPorts: host
                );

                global.Routes[0].Timeout = 4399;
                global.Routes[0].Priority = 3389;
                global.Routes[0].AddDelegatingHandler("Taitans");
                global.Routes[0].Key = "WO_CAO";
                global.Routes[0].UpstreamHost = "http://www.Taitans.com";
                global.Routes[0].HttpHandlerOption = new RouteHttpHandlerOption(global.Id, global.Routes[0].Name)
                {
                    AllowAutoRedirect = true,
                    UseCookieContainer = true,
                    UseProxy = true,
                    UseTracing = true,
                };
                global.Routes[0].AuthenticationOption = new RouteAuthenticationOption(global.Id, global.Routes[0].Name)
                {
                    AuthenticationProviderKey = "Taitans",
                };
                global.Routes[0].AuthenticationOption.AddScope("Taitans");

                global.Routes[0].RateLimitOption = new RouteRateLimitRule(global.Id, global.Routes[0].Name)
                {
                    Period = "Tatains",
                    PeriodTimespan = 0.1415926,
                    EnableRateLimiting = true,
                    Limit = 5201314,
                };
                global.Routes[0].RateLimitOption.AddWhitelist("Taitans");

                global.Routes[0].LoadBalancerOption = new RouteLoadBalancerOption(global.Id, global.Routes[0].Name)
                {
                    Key = "Taitans",
                    Type = "www.Taitans.com",
                    Expiry = 95
                };

                global.Routes[0].QoSOption = new RouteQoSOption(global.Id, global.Routes[0].Name)
                {
                    ExceptionsAllowedBeforeBreaking = 802,
                    DurationOfBreak = 24300,
                    TimeoutValue = 30624
                };

                global.Routes[0].ServiceName = "Taitans-cn";
                global.Routes[0].RouteIsCaseSensitive = true;
                global.Routes[0].CacheOption = new RouteCacheOption(global.Id, global.Routes[0].Name)
                {
                    TtlSeconds = 2020,
                    Region = "github.com/Taitans"
                };


                global.Routes[0].RequestIdKey = "ttgzs.cn";
                global.Routes[0].AddQueriesToRequests.Add("NB", "www.Taitans.com");
                global.Routes[0].RouteClaimsRequirements.Add("MVP", "www.Taitans.com");
                global.Routes[0].AddClaimsToRequests.Add("AT", "www.Taitans.com");
                global.Routes[0].DownstreamHeaderTransforms.Add("CVT", "www.Taitans.com");
                global.Routes[0].UpstreamHeaderTransforms.Add("DCT", "www.Taitans.com");
                global.Routes[0].AddHeadersToRequests.Add("Trubost", "www.Taitans.com");
                global.Routes[0].ChangeDownstreamPathTemplates.Add("EVCT", "www.Taitans.com");

                global.Routes[0].DangerousAcceptAnyServerCertificateValidator = true;

                global.Routes[0].SecurityOption = new RouteSecurityOption(global.Id, global.Routes[0].Name);
                global.Routes[0].SecurityOption.AddIPAllowed("199.88.88.88");
                global.Routes[0].SecurityOption.AddIPBlocked("88.88.88.88");

                var host2 = new Dictionary<string, int>();
                host2.Add("Taitans.middleground.httpapi.host", 80);
                global.AddRoutes(
                   "AllUrl",
                   "/{url}",
                   null,
                   "http",
                   "/{url}",
                   "GET",
                   upstreamHttpMethods: new List<string>() { "GET", "POST", "PUT", "DELETE", "OPTIONS" },
                   downstreamHostAndPorts: host2
               );
                await _ocelotRepository.InsertAsync(global);
            }
        }
    }
}