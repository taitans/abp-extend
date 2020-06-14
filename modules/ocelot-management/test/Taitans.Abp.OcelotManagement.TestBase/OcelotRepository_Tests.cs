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
            ocelot.Routes.Count.ShouldBeGreaterThanOrEqualTo(2);
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
            ocelot.Routes.Count.ShouldBeGreaterThanOrEqualTo(2);
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
            role.Routes.ShouldNotBeNull();
            role.Routes.Any().ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Route_Value_Check_True()
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

            ocelot.Routes.Count.ShouldBeGreaterThan(0);

            var route = ocelot.FindRoute("Token");
            route.Timeout.ShouldBe(4399);
            route.Priority.ShouldBe(3389);
            route.DelegatingHandlers.Count.ShouldBeGreaterThan(0);
            route.DelegatingHandlers[0].Delegating.ShouldBe("Taitans");
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
            route.UpstreamPathTemplate.ShouldBe("/connect/token");
            route.DownstreamPathTemplate.ShouldBe("/connect/token");
            route.DangerousAcceptAnyServerCertificateValidator.ShouldBe(true);
            route.SecurityOption.IPAllowedList.Count.ShouldBe(1);
            route.SecurityOption.IPBlockedList.Count.ShouldBe(1);
        }
    }
}
