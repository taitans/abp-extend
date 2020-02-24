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
        public async Task GetReRoutesAsync()
        {
            var ocelot = UsingDbContext(dbContext => dbContext.GlobalConfigurations.Single(t => t.Name == "middleware"));

            var route = await _ocelotAppService.GetReRoutesAsync(ocelot.Id);

            var reRoute = route.FirstOrDefault(c => c.Name == "Token");
            reRoute.Timeout.ShouldBe(4399);
            reRoute.Priority.ShouldBe(3389);
            reRoute.DelegatingHandlers.Count.ShouldBeGreaterThan(0);
            reRoute.DelegatingHandlers.ShouldContain("Taitans");
            reRoute.Key.ShouldBe("WO_CAO");
            reRoute.UpstreamHost.ShouldBe("http://www.taitans.com");
            reRoute.DownstreamHostAndPorts.ShouldNotBeNull();

            reRoute.HttpHandlerOption.ShouldNotBeNull();
            reRoute.HttpHandlerOption.AllowAutoRedirect.ShouldBe(true);
            reRoute.HttpHandlerOption.UseCookieContainer.ShouldBe(true);
            reRoute.HttpHandlerOption.UseProxy.ShouldBe(true);
            reRoute.HttpHandlerOption.UseTracing.ShouldBe(true);

            reRoute.AuthenticationOption.ShouldNotBeNull();
            reRoute.AuthenticationOption.AllowedScopes.Count.ShouldBe(1);
            reRoute.AuthenticationOption.AllowedScopes.ShouldContain("Taitans");
            reRoute.RateLimitOption.ShouldNotBeNull();
            reRoute.RateLimitOption.ClientWhitelist.Count.ShouldBe(1);
            reRoute.LoadBalancerOption.ShouldNotBeNull();
            reRoute.LoadBalancerOption.Expiry.ShouldBe(95);
            reRoute.LoadBalancerOption.Key.ShouldBe("Taitans");
            reRoute.LoadBalancerOption.Type.ShouldBe("www.taitans.com");
            reRoute.QoSOption.ShouldNotBeNull();
            reRoute.QoSOption.DurationOfBreak.ShouldBe(24300);
            reRoute.QoSOption.ExceptionsAllowedBeforeBreaking.ShouldBe(802);
            reRoute.QoSOption.TimeoutValue.ShouldBe(30624);
            reRoute.DownstreamScheme.ShouldBe("http");
            reRoute.ServiceName.ShouldBe("taitans-cn");
            reRoute.ReRouteIsCaseSensitive.ShouldBe(true);
            reRoute.CacheOption.ShouldNotBeNull();
            reRoute.CacheOption.TtlSeconds.ShouldBe(2020);
            reRoute.CacheOption.Region.ShouldBe("github.com/taitans");
            reRoute.RequestIdKey.ShouldNotBeNull("ttgzs.cn");
            reRoute.AddQueriesToRequests.ShouldContainKeyAndValue("NB", "www.taitans.com");
            reRoute.RouteClaimsRequirements.ShouldContainKeyAndValue("MVP", "www.taitans.com");
            reRoute.AddClaimsToRequests.ShouldContainKeyAndValue("AT", "www.taitans.com");
            reRoute.DownstreamHeaderTransforms.ShouldContainKeyAndValue("CVT", "www.taitans.com");
            reRoute.UpstreamHeaderTransforms.ShouldContainKeyAndValue("DCT", "www.taitans.com");
            reRoute.AddHeadersToRequests.ShouldContainKeyAndValue("Trubost", "www.taitans.com");
            reRoute.ChangeDownstreamPathTemplates.ShouldContainKeyAndValue("EVCT", "www.taitans.com");
            reRoute.UpstreamHttpMethods.Count.ShouldBe(1);
            reRoute.UpstreamPathTemplate.ShouldBe("/connect/token");
            reRoute.DownstreamPathTemplate.ShouldBe("/connect/token");
            reRoute.DangerousAcceptAnyServerCertificateValidator.ShouldBe(true);
            reRoute.SecurityOption.IPAllowedList.Count.ShouldBe(1);
            reRoute.SecurityOption.IPBlockedList.Count.ShouldBe(1);
        }

    }
}
