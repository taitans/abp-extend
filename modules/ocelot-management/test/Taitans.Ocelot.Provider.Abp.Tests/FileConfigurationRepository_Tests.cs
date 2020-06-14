using Ocelot.Cache;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;
using Shouldly;
using Taitans.Abp.OcelotManagement;
using Taitans.Ocelot.Provider.Abp.Configuration;
using Taitans.Ocelot.Provider.Abp.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;

namespace Taitans.Ocelot.Provider.Abp.Tests
{
    public class FileConfigurationRepository_Tests : AbpOcelotProviderAbpTestBase
    {
        private readonly IOcelotCache<FileConfiguration> _cachOptions;
        private readonly ConfigCacheOptions _configCacheOptions;
        private readonly IOcelotRepository _ocelotRepository;
        private IAbpFileConfigurationRepository _abpFileConfigurationRepository;
        private readonly IOcelotLoggerFactory _loggerFactory;
        private IFileConfigurationRepository _fileConfigRepository;

        public FileConfigurationRepository_Tests()
        {
            _cachOptions = Substitute.For<IOcelotCache<FileConfiguration>>(); 
            _ocelotRepository = GetRequiredService<IOcelotRepository>();
            _loggerFactory = Substitute.For<IOcelotLoggerFactory>();
            var logger = Substitute.For<IOcelotLogger>();
            _loggerFactory.CreateLogger<FileConfigurationRepository>().Returns(logger);


            _configCacheOptions = new ConfigCacheOptions { GatewayName = "middleware" };
        }

        private void GivenIHaveAConfiguration()
        {
            var loggerFactory = Substitute.For<IOcelotLoggerFactory>();
            var logger = Substitute.For<IOcelotLogger>();
            loggerFactory.CreateLogger<EfCoreFileConfigurationRepository>().Returns(logger);


            _abpFileConfigurationRepository = new EfCoreFileConfigurationRepository(_ocelotRepository, loggerFactory);
            _fileConfigRepository = new FileConfigurationRepository(
                _configCacheOptions,
                _cachOptions,
                _abpFileConfigurationRepository,
                _loggerFactory);
        }

        [Fact]
        public async Task Should_Get_Config()
        {
            GivenIHaveAConfiguration();
            var response = await _fileConfigRepository.Get();
            response.ShouldNotBeNull();
            response.Errors.Count.ShouldBe(0);
            response.IsError.ShouldBe(false);
            var routes = response.Data.Routes;
            var route = routes.FirstOrDefault(c => c.UpstreamPathTemplate.Equals("/connect/token"));
            route.Timeout.ShouldBe(4399);
            route.Priority.ShouldBe(3389);
            route.DelegatingHandlers.Count.ShouldBeGreaterThan(0);
            route.DelegatingHandlers[0].ShouldBe("Taitans");
            route.Key.ShouldBe("WO_CAO");
            route.UpstreamHost.ShouldBe("http://www.Taitans.com");
            route.DownstreamHostAndPorts.ShouldNotBeNull();

            route.HttpHandlerOptions.ShouldNotBeNull();
            route.HttpHandlerOptions.AllowAutoRedirect.ShouldBe(true);
            route.HttpHandlerOptions.UseCookieContainer.ShouldBe(true);
            route.HttpHandlerOptions.UseProxy.ShouldBe(true);
            route.HttpHandlerOptions.UseTracing.ShouldBe(true);

            route.AuthenticationOptions.ShouldNotBeNull();
            route.AuthenticationOptions.AllowedScopes.Count.ShouldBe(1);
            route.RateLimitOptions.ShouldNotBeNull();
            route.RateLimitOptions.ClientWhitelist.Count.ShouldBe(1);
            route.LoadBalancerOptions.ShouldNotBeNull();
            route.LoadBalancerOptions.Expiry.ShouldBe(95);
            route.LoadBalancerOptions.Key.ShouldBe("Taitans");
            route.LoadBalancerOptions.Type.ShouldBe("www.Taitans.com");
            route.QoSOptions.ShouldNotBeNull();
            route.QoSOptions.DurationOfBreak.ShouldBe(24300);
            route.QoSOptions.ExceptionsAllowedBeforeBreaking.ShouldBe(802);
            route.QoSOptions.TimeoutValue.ShouldBe(30624);
            route.DownstreamScheme.ShouldBe("http");
            route.DownstreamHttpMethod.ShouldBe("GET");
            route.ServiceName.ShouldBe("Taitans-cn");
            route.RouteIsCaseSensitive.ShouldBe(true);
            route.FileCacheOptions.ShouldNotBeNull();
            route.FileCacheOptions.TtlSeconds.ShouldBe(2020);
            route.FileCacheOptions.Region.ShouldBe("github.com/Taitans");
            route.RequestIdKey.ShouldNotBeNull("ttgzs.cn");
            route.AddQueriesToRequest.ShouldContainKeyAndValue("NB", "www.Taitans.com");
            route.RouteClaimsRequirement.ShouldContainKeyAndValue("MVP", "www.Taitans.com");
            route.AddClaimsToRequest.ShouldContainKeyAndValue("AT", "www.Taitans.com");
            route.DownstreamHeaderTransform.ShouldContainKeyAndValue("CVT", "www.Taitans.com");
            route.UpstreamHeaderTransform.ShouldContainKeyAndValue("DCT", "www.Taitans.com");
            route.AddHeadersToRequest.ShouldContainKeyAndValue("Trubost", "www.Taitans.com");
            route.ChangeDownstreamPathTemplate.ShouldContainKeyAndValue("EVCT", "www.Taitans.com");
            route.UpstreamHost.ShouldBe("http://www.Taitans.com");
            route.UpstreamHttpMethod.Count.ShouldBe(1);
            route.UpstreamPathTemplate.ShouldBe("/connect/token");
            route.DownstreamPathTemplate.ShouldBe("/connect/token");
            route.DangerousAcceptAnyServerCertificateValidator.ShouldBe(true);
            route.SecurityOptions.IPAllowedList.Count.ShouldBe(1);
            route.SecurityOptions.IPBlockedList.Count.ShouldBe(1);
        }

        [Fact]
        public Task Should_Automatically_Register_EventHandlers_From_Services()
        {
            //TODO: Workaround.
            return Task.CompletedTask;
        }
    }
}
