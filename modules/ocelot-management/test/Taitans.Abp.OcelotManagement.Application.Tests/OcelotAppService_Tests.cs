using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotAppService_Tests : AbpOcelotManagementApplicationTestBase
    {
        private readonly IOcelotAppService _ocelotAppService;

        public OcelotAppService_Tests()
        {
            _ocelotAppService = GetRequiredService<IOcelotAppService>();
        }

        [Fact]
        public async Task GetAsync()
        {
            var ocelotInDb = UsingDbContext(dbContext => dbContext.GlobalConfigurations.First());
            var ocelotGlobal = await _ocelotAppService.GetAsync(ocelotInDb.Id).ConfigureAwait(false);
            ocelotGlobal.Name.ShouldBe(ocelotInDb.Name);
        }

        [Fact]
        public async Task GetListAsync()
        {
            var result = await _ocelotAppService.GetListAsync(new PagedAndSortedResultRequestDto()).ConfigureAwait(false);
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(t => t.Name == "middleware");
        }

        [Fact]
        public async Task GetListAsync_Sorted_Descending_By_Name()
        {
            var result = await _ocelotAppService.GetListAsync(new PagedAndSortedResultRequestDto { Sorting = "Name DESC" }).ConfigureAwait(false);
            result.TotalCount.ShouldBeGreaterThan(0);
            var configs = result.Items.ToList();

            configs.ShouldContain(t => t.Name == "middleware");

            configs.FindIndex(t => t.Name == "middleware").ShouldBeGreaterThanOrEqualTo(configs.FindIndex(t => t.Name == "middleware"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            var gatewayName = Guid.NewGuid().ToString("N").ToLowerInvariant();
            var config = await _ocelotAppService.CreateAsync(new OcelotCreateDto { Name = gatewayName }).ConfigureAwait(false);
            config.Name.ShouldBe(gatewayName);
            config.Id.ShouldNotBe(default);
        }

        [Fact]
        public async Task CreateAsync_Should_Not_Allow_Duplicate_Names()
        {
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _ocelotAppService.CreateAsync(new OcelotCreateDto { Name = "middleware" }).ConfigureAwait(false);

                var list = UsingDbContext(dbContext => dbContext.GlobalConfigurations.ToList());
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var ocelot = UsingDbContext(dbContext => dbContext.GlobalConfigurations.Single(t => t.Name == "middleware"));

            var result = await _ocelotAppService.UpdateAsync(ocelot.Id, new OcelotUpdateDto { RequestIdKey = "acme-renamed" }).ConfigureAwait(false);
            result.Id.ShouldBe(ocelot.Id);
            result.RequestIdKey.ShouldBe("acme-renamed");

            var acmeUpdated = UsingDbContext(dbContext => dbContext.GlobalConfigurations.Single(t => t.Id == ocelot.Id));
            acmeUpdated.RequestIdKey.ShouldBe("acme-renamed");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var ocelot = UsingDbContext(dbContext => dbContext.GlobalConfigurations.Single(t => t.Name == "middleware"));

            await _ocelotAppService.DeleteAsync(ocelot.Id).ConfigureAwait(false);

            UsingDbContext(dbContext =>
            {
                dbContext.GlobalConfigurations.Any(t => t.Id == ocelot.Id).ShouldBeFalse();
            });
        }

        [Fact]
        public async Task GetRoutesAsync()
        {
            var ocelot = UsingDbContext(dbContext => dbContext.GlobalConfigurations.Single(t => t.Name == "middleware"));

            var routes = await _ocelotAppService.GetRoutesAsync(ocelot.Id);

            var route = routes.FirstOrDefault(c => c.Name == "Token");
            route.Timeout.ShouldBe(4399);
            route.Priority.ShouldBe(3389);
            route.DelegatingHandlers.Count.ShouldBeGreaterThan(0);
            route.DelegatingHandlers.ShouldContain("Taitans");
            route.Key.ShouldBe("WO_CAO");
            route.UpstreamHost.ShouldBe("http://www.Taitans.com");
            route.DownstreamHostAndPorts.ShouldNotBeNull();

            route.HttpHandlerOption.ShouldNotBeNull();
            route.HttpHandlerOption.AllowAutoRedirect.ShouldBe(true);
            route.HttpHandlerOption.UseCookieContainer.ShouldBe(true);
            route.HttpHandlerOption.UseProxy.ShouldBe(true);
            route.HttpHandlerOption.UseTracing.ShouldBe(true);

            route.AuthenticationOption.ShouldNotBeNull();
            route.AuthenticationOption.AllowedScopes.Count.ShouldBe(1);
            route.AuthenticationOption.AllowedScopes.ShouldContain("Taitans");
            route.RateLimitOption.ShouldNotBeNull();
            route.RateLimitOption.ClientWhitelist.Count.ShouldBe(1);
            route.LoadBalancerOption.ShouldNotBeNull();
            route.LoadBalancerOption.Expiry.ShouldBe(95);
            route.LoadBalancerOption.Key.ShouldBe("Taitans");
            route.LoadBalancerOption.Type.ShouldBe("www.Taitans.com");
            route.QoSOption.ShouldNotBeNull();
            route.QoSOption.DurationOfBreak.ShouldBe(24300);
            route.QoSOption.ExceptionsAllowedBeforeBreaking.ShouldBe(802);
            route.QoSOption.TimeoutValue.ShouldBe(30624);
            route.DownstreamScheme.ShouldBe("http");
            route.ServiceName.ShouldBe("Taitans-cn");
            route.RouteIsCaseSensitive.ShouldBe(true);
            route.CacheOption.ShouldNotBeNull();
            route.CacheOption.TtlSeconds.ShouldBe(2020);
            route.CacheOption.Region.ShouldBe("github.com/Taitans");
            route.RequestIdKey.ShouldNotBeNull("ttgzs.cn");
            route.AddQueriesToRequests.ShouldContainKeyAndValue("NB", "www.Taitans.com");
            route.RouteClaimsRequirements.ShouldContainKeyAndValue("MVP", "www.Taitans.com");
            route.AddClaimsToRequests.ShouldContainKeyAndValue("AT", "www.Taitans.com");
            route.DownstreamHeaderTransforms.ShouldContainKeyAndValue("CVT", "www.Taitans.com");
            route.UpstreamHeaderTransforms.ShouldContainKeyAndValue("DCT", "www.Taitans.com");
            route.AddHeadersToRequests.ShouldContainKeyAndValue("Trubost", "www.Taitans.com");
            route.ChangeDownstreamPathTemplates.ShouldContainKeyAndValue("EVCT", "www.Taitans.com");
            route.UpstreamHttpMethods.Count.ShouldBe(1);
            route.UpstreamHttpMethods.ShouldContain("POST");
            route.UpstreamPathTemplate.ShouldBe("/connect/token");
            route.DownstreamPathTemplate.ShouldBe("/connect/token");
            route.DangerousAcceptAnyServerCertificateValidator.ShouldBe(true);
            route.SecurityOption.IPAllowedList.Count.ShouldBe(1);
            route.SecurityOption.IPBlockedList.Count.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateRouteAsync()
        {
            var ocelot = UsingDbContext(dbContext => dbContext.GlobalConfigurations.Single(t => t.Name == "middleware"));

            await _ocelotAppService.UpdateRoutesAsync(ocelot.Id, new System.Collections.Generic.List<OcelotRouteDto>() {
                new OcelotRouteDto()
                {
                    Name="Token",
                    UpstreamPathTemplate = "/taitans/com",
                    UpstreamHost = "http://ttgzs.cn",
                    DownstreamScheme ="https",
                    DownstreamPathTemplate  = "/ttgzs/net",
                    DownstreamHostAndPorts = new System.Collections.Generic.List<RouteDownstreamHostAndPortDto>()
                    {
                       new RouteDownstreamHostAndPortDto(){ Host = "www.taitans.com", Port = 80}
                    },
                    UpstreamHttpMethods = new System.Collections.Generic.List<string>{ "DDD" },
                    DelegatingHandlers = new System.Collections.Generic.List<string>{ "ttgzs" },
                    Timeout = 44300,
                    Priority = 65535,
                    Key = "BIU_TE_FOU",
                    HttpHandlerOption = new RouteHttpHandlerOptionDto
                    {
                        AllowAutoRedirect = false,
                        UseCookieContainer = false,
                        UseProxy = true,
                        UseTracing = false,
                    },
                    AuthenticationOption = new RouteAuthenticationOptionDto
                    {
                        AuthenticationProviderKey ="TTGZS",
                        AllowedScopes = new System.Collections.Generic.List<string>
                        {
                            "Studio",
                            "Ttgzs"
                        }
                    },
                    RateLimitOption = new RouteRateLimitRuleDto
                    {
                        Period = "Ttgzs",
                        PeriodTimespan = 3.14156666,
                        EnableRateLimiting = false,
                        Limit = 10086,
                        ClientWhitelist = new System.Collections.Generic.List<string>
                        {
                            "cn"
                        }
                    },
                    LoadBalancerOption = new RouteLoadBalancerOptionDto
                    {
                        Key = "ttgzs",
                        Type = "www.ttgzs.cn",
                        Expiry = 9595
                    },
                    QoSOption = new RouteQoSOptionDto
                    {
                        ExceptionsAllowedBeforeBreaking = 802,
                        DurationOfBreak = 24300,
                        TimeoutValue = 30624
                    },
                    ServiceName = "ttgzs.cn",
                    RouteIsCaseSensitive = false,
                    CacheOption = new RouteCacheOptionDto
                    {
                        TtlSeconds = 2020,
                        Region = "github.com/loongle"
                    },
                    RequestIdKey = "taitans.com",

                    DangerousAcceptAnyServerCertificateValidator = false,
                    SecurityOption = new RouteSecurityOptionDto
                    {
                        IPAllowedList = new System.Collections.Generic.List<string>{"123.123.123.123"},
                        IPBlockedList = new System.Collections.Generic.List<string>{"114.114.114.114"}
                    }
                },
                new OcelotRouteDto()
                {
                    Name="Loongle",
                    UpstreamPathTemplate ="/taitans/loognle",
                    UpstreamHost = null,
                    DownstreamScheme ="http",
                    DownstreamPathTemplate  = "/ttgzs/loongle",
                    DownstreamHostAndPorts = new System.Collections.Generic.List<RouteDownstreamHostAndPortDto>()
                    {
                       new RouteDownstreamHostAndPortDto(){ Host ="www.ttgzs.net",Port=80}
                    }
                }
            });

            var result = await _ocelotAppService.GetRoutesAsync(ocelot.Id);
            result.Count.ShouldBe(2);
            var token = result.Find(c => c.Name == "Token");
            token.ShouldNotBeNull();
            token.UpstreamPathTemplate.ShouldBe("/taitans/com");
            token.UpstreamHost.ShouldBe("http://ttgzs.cn");
            token.DownstreamScheme.ShouldBe("https");
            token.DownstreamPathTemplate.ShouldBe("/ttgzs/net");
            token.DownstreamHostAndPorts.ShouldContain(c => c.Host == "www.taitans.com" && c.Port == 80);
            token.DelegatingHandlers.ShouldContain("ttgzs");
            token.Timeout.ShouldBe(44300);
            token.Priority.ShouldBe(65535);
            token.Key.ShouldBe("BIU_TE_FOU");
            token.UpstreamHttpMethods.Count.ShouldBe(1);
            token.UpstreamHttpMethods.ShouldContain("DDD");

            token.HttpHandlerOption.AllowAutoRedirect.ShouldBe(false);
            token.HttpHandlerOption.UseCookieContainer.ShouldBe(false);
            token.HttpHandlerOption.UseProxy.ShouldBe(true);
            token.HttpHandlerOption.UseTracing.ShouldBe(false);

            token.AuthenticationOption.AuthenticationProviderKey.ShouldBe("TTGZS");
            token.AuthenticationOption.AllowedScopes.Count.ShouldBe(2);
            token.AuthenticationOption.AllowedScopes.ShouldContain("Studio");
            token.AuthenticationOption.AllowedScopes.ShouldContain("Ttgzs");

            token.RateLimitOption.Period.ShouldBe("Ttgzs");
            token.RateLimitOption.PeriodTimespan.ShouldBe(3.14156666);
            token.RateLimitOption.EnableRateLimiting.ShouldBe(false);
            token.RateLimitOption.Limit.ShouldBe(10086);
            token.RateLimitOption.ClientWhitelist.Count.ShouldBe(1);
            token.RateLimitOption.ClientWhitelist.ShouldContain("cn");

            token.LoadBalancerOption.Key.ShouldBe("ttgzs");
            token.LoadBalancerOption.Type.ShouldBe("www.ttgzs.cn");
            token.LoadBalancerOption.Expiry.ShouldBe(9595);

            token.QoSOption.ExceptionsAllowedBeforeBreaking.ShouldBe(802);
            token.QoSOption.DurationOfBreak.ShouldBe(24300);
            token.QoSOption.TimeoutValue.ShouldBe(30624);

            token.ServiceName.ShouldBe("ttgzs.cn");
            token.RouteIsCaseSensitive.ShouldBe(false);

            token.CacheOption.TtlSeconds.ShouldBe(2020);
            token.CacheOption.Region.ShouldBe("github.com/loongle");

            token.RequestIdKey.ShouldBe("taitans.com");
            token.DangerousAcceptAnyServerCertificateValidator.ShouldBe(false);

            token.SecurityOption.IPAllowedList.ShouldContain("123.123.123.123");
            token.SecurityOption.IPBlockedList.ShouldContain("114.114.114.114");

            result.ShouldContain(c => c.Name == "Loongle" && c.DownstreamScheme == "http");
            result.ShouldContain(c => c.UpstreamPathTemplate == "/taitans/loognle" && c.DownstreamPathTemplate == "/ttgzs/loongle");
            result.ShouldContain(c => c.UpstreamPathTemplate == "/taitans/com" && c.DownstreamPathTemplate == "/ttgzs/net");

        }


    }
}
