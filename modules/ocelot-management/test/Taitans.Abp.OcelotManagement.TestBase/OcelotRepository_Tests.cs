using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotRepository_Tests<TStartupModule> :
        OcelotManagementTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        public IOcelotRepository OcelotRepository { get; }

        protected OcelotRepository_Tests()
        {
            OcelotRepository = GetRequiredService<IOcelotRepository>();
        }

        [Fact]
        public async Task FindByNameAsync()
        {
            var ocelot = await OcelotRepository.FindByNameAsync("middleware");
            ocelot.ShouldNotBeNull();

            ocelot = await OcelotRepository.FindByNameAsync("undefined-ocelot");
            ocelot.ShouldBeNull();

            ocelot = await OcelotRepository.FindByNameAsync("middleware", includeDetails: true);
            ocelot.ShouldNotBeNull();
            ocelot.ReRoutes.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task FindAsync()
        {
            var ocelotId = (await OcelotRepository.FindByNameAsync("middleware")).Id;

            var ocelot = await OcelotRepository.FindAsync(ocelotId);
            ocelot.ShouldNotBeNull();

            ocelot = await OcelotRepository.FindAsync(Guid.NewGuid());
            ocelot.ShouldBeNull();

            ocelot = await OcelotRepository.FindAsync(ocelotId, includeDetails: true);
            ocelot.ShouldNotBeNull();
            ocelot.ReRoutes.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task GetListAsync()
        {
            var ocelots = await OcelotRepository.GetListAsync();
            ocelots.ShouldContain(t => t.Name == "middleware");
        }

        [Fact]
        public async Task Should_Eager_Load_Ocelot_Collections()
        {
            var role = await OcelotRepository.FindByNameAsync("middleware");
            role.ReRoutes.ShouldNotBeNull();
            role.ReRoutes.Any().ShouldBeTrue();
        }

        [Fact]
        public async Task Should_ReRoute_Value_Check_True()
        {

            var ocelot = await OcelotRepository.FindByNameAsync("middleware").ConfigureAwait(false);

            ocelot.RequestIdKey.ShouldNotBeNull();
            ocelot.ServiceDiscoveryProvider.ShouldNotBeNull("192.168.88.88");
            ocelot.ServiceDiscoveryProvider.ConfigurationKey.ShouldBe("Taitans-NB");
            ocelot.ServiceDiscoveryProvider.PollingInterval.ShouldBe(8888);
            ocelot.ServiceDiscoveryProvider.Port.ShouldBe(2888);
            ocelot.ServiceDiscoveryProvider.Token.ShouldBe("Taitans-Token");
            ocelot.ServiceDiscoveryProvider.Type.ShouldBe("Taitans");
            ocelot.ServiceDiscoveryProvider.Namespace.ShouldBe("TaitansStudio");

            ocelot.RateLimitOption.ShouldNotBeNull();
            ocelot.QoSOption.ShouldNotBeNull();
            ocelot.BaseUrl.ShouldNotBeNull();
            ocelot.LoadBalancerOption.ShouldNotBeNull();
            ocelot.DownstreamScheme.ShouldNotBeNull();
            ocelot.HttpHandlerOption.ShouldNotBeNull();

            ocelot.ReRoutes.Count.ShouldBeGreaterThan(0);

            var reRoute = ocelot.FindReRoute("Token");
            reRoute.Timeout.ShouldBe(4399);
            reRoute.Priority.ShouldBe(3389);
            reRoute.DelegatingHandlers.Count.ShouldBeGreaterThan(0);
            reRoute.DelegatingHandlers[0].Delegating.ShouldBe("Taitans");
            reRoute.Key.ShouldBe("WO_CAO");
            reRoute.UpstreamHost.ShouldBe("http://www.Taitans.com");
            reRoute.DownstreamHostAndPorts.ShouldNotBeNull();

            reRoute.HttpHandlerOption.ShouldNotBeNull();
            reRoute.HttpHandlerOption.AllowAutoRedirect.ShouldBe(true);
            reRoute.HttpHandlerOption.UseCookieContainer.ShouldBe(true);
            reRoute.HttpHandlerOption.UseProxy.ShouldBe(true);
            reRoute.HttpHandlerOption.UseTracing.ShouldBe(true);

            reRoute.AuthenticationOption.ShouldNotBeNull();
            reRoute.AuthenticationOption.AllowedScopes.Count.ShouldBe(1);
            reRoute.RateLimitOption.ShouldNotBeNull();
            reRoute.RateLimitOption.ClientWhitelist.Count.ShouldBe(1);
            reRoute.LoadBalancerOption.ShouldNotBeNull();
            reRoute.LoadBalancerOption.Expiry.ShouldBe(95);
            reRoute.LoadBalancerOption.Key.ShouldBe("Taitans");
            reRoute.LoadBalancerOption.Type.ShouldBe("www.Taitans.com");
            reRoute.QoSOption.ShouldNotBeNull();
            reRoute.QoSOption.DurationOfBreak.ShouldBe(24300);
            reRoute.QoSOption.ExceptionsAllowedBeforeBreaking.ShouldBe(802);
            reRoute.QoSOption.TimeoutValue.ShouldBe(30624);
            reRoute.DownstreamScheme.ShouldBe("http");
            reRoute.ServiceName.ShouldBe("Taitans-cn");
            reRoute.ReRouteIsCaseSensitive.ShouldBe(true);
            reRoute.CacheOption.ShouldNotBeNull();
            reRoute.CacheOption.TtlSeconds.ShouldBe(2020);
            reRoute.CacheOption.Region.ShouldBe("github.com/Taitans");
            reRoute.RequestIdKey.ShouldNotBeNull("ttgzs.cn");
            reRoute.AddQueriesToRequests.ShouldContainKeyAndValue("NB", "www.Taitans.com");
            reRoute.RouteClaimsRequirements.ShouldContainKeyAndValue("MVP", "www.Taitans.com");
            reRoute.AddClaimsToRequests.ShouldContainKeyAndValue("AT", "www.Taitans.com");
            reRoute.DownstreamHeaderTransforms.ShouldContainKeyAndValue("CVT", "www.Taitans.com");
            reRoute.UpstreamHeaderTransforms.ShouldContainKeyAndValue("DCT", "www.Taitans.com");
            reRoute.AddHeadersToRequests.ShouldContainKeyAndValue("Trubost", "www.Taitans.com");
            reRoute.ChangeDownstreamPathTemplates.ShouldContainKeyAndValue("EVCT", "www.Taitans.com");
            reRoute.UpstreamHttpMethods.Count.ShouldBe(1);
            reRoute.UpstreamPathTemplate.ShouldBe("/connect/token");
            reRoute.DownstreamPathTemplate.ShouldBe("/connect/token");
            reRoute.DangerousAcceptAnyServerCertificateValidator.ShouldBe(true);
            reRoute.SecurityOption.IPAllowedList.Count.ShouldBe(1);
            reRoute.SecurityOption.IPBlockedList.Count.ShouldBe(1);
        }
    }
}
