using Microsoft.AspNetCore.Authorization;
using Taitans.Abp.OcelotManagement.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;
using System.Linq;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotAppService : CrudAppService<
             Ocelot,
             OcelotDto,
             Guid,
             PagedAndSortedResultRequestDto,
             OcelotCreateDto,
             OcelotUpdateDto>, IOcelotAppService
    {
        protected IOcelotRepository OcelotRepository { get; }
        protected IOcelotManager OcelotManager { get; }
        public IDistributedEventBus DistributedEventBus { get; }

        public OcelotAppService(IOcelotRepository ocelotRepository,
            IOcelotManager ocelotManager,
            IDistributedEventBus distributedEventBus) : base(ocelotRepository)
        {
            OcelotRepository = ocelotRepository;
            OcelotManager = ocelotManager;
            DistributedEventBus = distributedEventBus;
        }

        public override async Task<OcelotDto> GetAsync(Guid id)
        {
            var entity = await OcelotRepository.GetAsync(id).ConfigureAwait(false);
            return ObjectMapper.Map<Ocelot, OcelotDto>(entity);
        }

        public virtual async Task<OcelotWithDetailsDto> FindByNameAsync(string name)
        {
            var config = await OcelotRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Ocelot, OcelotWithDetailsDto>(config);
        }

        public virtual async Task<List<OcelotRouteDto>> GetRoutesAsync(Guid id)
        {
            var routes = await OcelotRepository.GetRoutesAsync(id);

            return ObjectMapper.Map<List<OcelotRoute>, List<OcelotRouteDto>>(routes);
        }

        public virtual async Task<List<OcelotRouteDto>> UpdateRoutesAsync(Guid id, List<OcelotRouteDto> input)
        {
            var ocelot = await OcelotRepository.GetAsync(id).ConfigureAwait(false);

            UpdateOcelotRoute(input, ocelot);

            await OcelotRepository.UpdateAsync(ocelot, true).ConfigureAwait(false);

            return ObjectMapper.Map<List<OcelotRoute>, List<OcelotRouteDto>>(ocelot.Routes);
        }

        protected virtual void UpdateOcelotRoute(List<OcelotRouteDto> input, Ocelot ocelot)
        {
            if (input == null)
            {
                ocelot.RemoveAllRoutes();
                return;
            }

            foreach (var routeDto in input)
            {
                var existing = ocelot.FindRoute(routeDto.Name);
                if (existing == null)
                {
                    var route = new OcelotRoute(
                             ocelot.Id,
                             routeDto.Name,
                             routeDto.UpstreamHost,
                             routeDto.UpstreamPathTemplate,
                             routeDto.DownstreamHttpMethod,
                             routeDto.DownstreamPathTemplate,
                             routeDto.DownstreamScheme,
                             routeDto.Key,
                             routeDto.ServiceNamespace,
                             routeDto.ServiceName,
                             routeDto.RouteIsCaseSensitive ?? false,
                             routeDto.RequestIdKey,
                             routeDto.DangerousAcceptAnyServerCertificateValidator ?? false,
                             routeDto.Timeout,
                             routeDto.Sort ?? 100,
                             routeDto.Priority ?? 1);


                    route.HttpHandlerOption = GetRouteHttpHandlerOption(ocelot.Id, routeDto.Name, routeDto.HttpHandlerOption);
                    route.AuthenticationOption = GetRouteAuthenticationOption(ocelot.Id, routeDto.Name, routeDto.AuthenticationOption);
                    route.RateLimitOption = GetRouteRateLimitRule(ocelot.Id, routeDto.Name, routeDto.RateLimitOption);
                    route.LoadBalancerOption = GetRouteLoadBalancerOption(ocelot.Id, routeDto.Name, routeDto.LoadBalancerOption);
                    route.QoSOption = GetRouteQoSOption(ocelot.Id, routeDto.Name, routeDto.QoSOption);
                    route.CacheOption = GetRouteCacheOption(ocelot.Id, routeDto.Name, routeDto.CacheOption);

                    UpdateDelegatingHandlers(routeDto, route);
                    UpdateDownstreamHostAndPorts(routeDto, route);
                    UpdateUpstreamHttpMethods(routeDto, route);
                    UpdateSecurityOption(routeDto, route);

                    ocelot.Routes.Add(route);

                }
                else
                {
                    existing.UpstreamHost = routeDto.UpstreamHost;
                    existing.UpstreamPathTemplate = routeDto.UpstreamPathTemplate;
                    existing.DownstreamScheme = routeDto.DownstreamScheme;
                    existing.DownstreamPathTemplate = routeDto.DownstreamPathTemplate;
                    existing.DownstreamHttpMethod = routeDto.DownstreamHttpMethod;
                    existing.Key = routeDto.Key;
                    existing.ServiceNamespace = routeDto.ServiceNamespace;
                    existing.ServiceName = routeDto.ServiceName;
                    existing.RouteIsCaseSensitive = routeDto.RouteIsCaseSensitive ?? false;
                    existing.RequestIdKey = routeDto.RequestIdKey;
                    existing.DangerousAcceptAnyServerCertificateValidator = routeDto.DangerousAcceptAnyServerCertificateValidator ?? false;
                    existing.Timeout = routeDto.Timeout;
                    existing.Sort = routeDto.Sort ?? 100;
                    existing.Priority = routeDto.Priority ?? 1;

                    existing.HttpHandlerOption = GetRouteHttpHandlerOption(ocelot.Id, routeDto.Name, routeDto.HttpHandlerOption);
                    existing.AuthenticationOption = GetRouteAuthenticationOption(ocelot.Id, routeDto.Name, routeDto.AuthenticationOption);
                    existing.RateLimitOption = GetRouteRateLimitRule(ocelot.Id, routeDto.Name, routeDto.RateLimitOption);
                    existing.LoadBalancerOption = GetRouteLoadBalancerOption(ocelot.Id, routeDto.Name, routeDto.LoadBalancerOption);
                    existing.QoSOption = GetRouteQoSOption(ocelot.Id, routeDto.Name, routeDto.QoSOption);
                    existing.CacheOption = GetRouteCacheOption(ocelot.Id, routeDto.Name, routeDto.CacheOption);

                    UpdateDelegatingHandlers(routeDto, existing);
                    UpdateDownstreamHostAndPorts(routeDto, existing);
                    UpdateUpstreamHttpMethods(routeDto, existing);
                    UpdateSecurityOption(routeDto, existing);
                }
            }

            //TODO Handle Update state

            //Copied with ToList to avoid modification of the collection in the for loop
            foreach (var route in ocelot.Routes.ToList())
            {
                if (!input.Any(c => route.Equals(route.GlobalConfigurationId, c.Name)))
                {
                    ocelot.RemoveRoute(route.Name);
                }
            }
        }

        private void UpdateDelegatingHandlers(OcelotRouteDto input, OcelotRoute route)
        {
            if (input.DelegatingHandlers == null)
            {
                input.DelegatingHandlers = null;
                return;
            }

            foreach (var delegating in input.DelegatingHandlers)
            {
                var existing = route.FindDelegatingHandler(delegating);
                if (existing == null)
                {
                    route.AddDelegatingHandler(delegating);
                }
            }

            //TODO Copied with ToList to avoid modification of the collection in the for loop
            foreach (var delegating in route.DelegatingHandlers.ToList())
            {
                if (!input.DelegatingHandlers.Any(c => delegating.Equals(route.GlobalConfigurationId, route.Name, c)))
                {
                    route.RemoveDelegatingHandlers(delegating.Delegating);
                }
            }
        }

        private void UpdateSecurityOption(OcelotRouteDto input, OcelotRoute route)
        {
            if (input.SecurityOption == null)
            {
                route.SecurityOption = null;
                return;
            }

            if (route.SecurityOption == null)
            {
                route.SecurityOption = new RouteSecurityOption(route.GlobalConfigurationId, route.Name);
            }

            UpdateSecurityOptionIPBlocked(input.SecurityOption, route.SecurityOption);
            UpdateSecurityOptionIPAllowed(input.SecurityOption, route.SecurityOption);
        }

        private void UpdateSecurityOptionIPAllowed(RouteSecurityOptionDto input, RouteSecurityOption securityOption)
        {

            foreach (var allowIp in input.IPAllowedList)
            {
                var existing = securityOption.FindIPAllowed(allowIp);
                if (existing == null)
                {
                    securityOption.AddIPAllowed(allowIp);
                }
            }

            //TODO Copied with ToList to avoid modification of the collection in the for loop
            foreach (var allow in securityOption.IPAllowedList.ToList())
            {
                if (!input.IPAllowedList.Any(c => allow.Equals(securityOption.GlobalConfigurationId, securityOption.RouteName, c)))
                {
                    securityOption.RemoveIPAllowed(allow.IP);
                }
            }
        }

        private void UpdateSecurityOptionIPBlocked(RouteSecurityOptionDto input, RouteSecurityOption securityOption)
        {
            foreach (var blockIp in input.IPBlockedList)
            {
                var existing = securityOption.FindIPBlocked(blockIp);
                if (existing == null)
                {
                    securityOption.AddIPBlocked(blockIp);
                }
            }

            //TODO Copied with ToList to avoid modification of the collection in the for loop
            foreach (var block in securityOption.IPBlockedList.ToList())
            {
                if (!input.IPBlockedList.Any(c => block.Equals(securityOption.GlobalConfigurationId, securityOption.RouteName, c)))
                {
                    securityOption.RemoveIPBlocked(block.IP);
                }
            }
        }

        private void UpdateUpstreamHttpMethods(OcelotRouteDto input, OcelotRoute route)
        {
            if (input.UpstreamHttpMethods == null)
            {
                route.RemoveAllUpstreamHttpMethods();
                return;
            }

            foreach (var method in input.UpstreamHttpMethods)
            {
                var existing = route.FindUpstreamHttpMethod(method);
                if (existing == null)
                {
                    route.AddUpstreamHttpMethod(method);
                }
            }

            //TODO Copied with ToList to avoid modification of the collection in the for loop
            foreach (var method in route.UpstreamHttpMethods.ToList())
            {
                if (!input.UpstreamHttpMethods.Any(c => method.Equals(route.GlobalConfigurationId, route.Name, c)))
                {
                    route.RemoveUpstreamHttpMethod(method.Method);
                }
            }
        }

        private RouteHttpHandlerOption GetRouteHttpHandlerOption(Guid globalConfigurationId, string routeName, RouteHttpHandlerOptionDto input)
        {
            if (input != null)
            {
                var routeHttpHandlerOption = new RouteHttpHandlerOption(globalConfigurationId, routeName)
                {
                    AllowAutoRedirect = input.AllowAutoRedirect ?? false,
                    MaxConnectionsPerServer = input.MaxConnectionsPerServer ?? int.MaxValue,
                    UseCookieContainer = input.UseCookieContainer ?? false,
                    UseProxy = input.UseProxy ?? true,
                    UseTracing = input.UseTracing ?? false,
                };

                return routeHttpHandlerOption;
            }

            return null;
        }

        private void UpdateDownstreamHostAndPorts(OcelotRouteDto input, OcelotRoute route)
        {
            if (input.DownstreamHostAndPorts == null)
            {
                route.RemoveAllDownstreamHostAndPorts();
                return;
            }

            foreach (var host in input.DownstreamHostAndPorts)
            {
                var existing = route.FindDownstreamHostAndPort(host.Host, host.Port.Value);
                if (existing == null)
                {
                    route.AddDownstreamHostAndPort(host.Host, host.Port.Value);
                }
            }

            //TODO Copied with ToList to avoid modification of the collection in the for loop
            foreach (var host in route.DownstreamHostAndPorts.ToList())
            {
                if (!input.DownstreamHostAndPorts.Any(c => host.Equals(route.GlobalConfigurationId, route.Name, c.Host, c.Port.Value)))
                {
                    route.RemoveDownstreamHostAndPort(host.Host, host.Port);
                }
            }
        }

        private RouteRateLimitRule GetRouteRateLimitRule(Guid globalConfigurationId, string routeName, RouteRateLimitRuleDto input)
        {
            if (input != null)
            {
                var rateLimitRule = new RouteRateLimitRule(globalConfigurationId, routeName)
                {
                    EnableRateLimiting = input.EnableRateLimiting.Value,
                    Limit = input.Limit.Value,
                    Period = input.Period,
                    PeriodTimespan = input.PeriodTimespan.Value
                };

                input.ClientWhitelist?.ForEach(white => rateLimitRule.AddWhitelist(white));

                return rateLimitRule;
            }

            return null;
        }

        private RouteLoadBalancerOption GetRouteLoadBalancerOption(Guid globalConfigurationId, string routeName, RouteLoadBalancerOptionDto input)
        {
            if (input != null)
            {
                var loadBalancerOption = new RouteLoadBalancerOption(globalConfigurationId, routeName)
                {
                    Type = input.Type,
                    Key = input.Key,
                    Expiry = input.Expiry.Value
                };

                return loadBalancerOption;
            }

            return null;
        }

        private RouteQoSOption GetRouteQoSOption(Guid globalConfigurationId, string routeName, RouteQoSOptionDto input)
        {
            if (input != null)
            {
                var qosOption = new RouteQoSOption(globalConfigurationId, routeName)
                {
                    ExceptionsAllowedBeforeBreaking = input.ExceptionsAllowedBeforeBreaking,
                    DurationOfBreak = input.DurationOfBreak,
                    TimeoutValue = input.TimeoutValue,
                };

                return qosOption;
            }

            return null;
        }

        private RouteAuthenticationOption GetRouteAuthenticationOption(Guid globalConfigurationId, string routeName, RouteAuthenticationOptionDto input)
        {
            if (input != null)
            {
                var authenticationOption = new RouteAuthenticationOption(globalConfigurationId, routeName)
                {
                    AuthenticationProviderKey = input.AuthenticationProviderKey
                };

                input.AllowedScopes?.ForEach(scope => authenticationOption.AddScope(scope));

                return authenticationOption;
            }

            return null;
        }

        private RouteCacheOption GetRouteCacheOption(Guid globalConfigurationId, string routeName, RouteCacheOptionDto input)
        {
            if (input != null)
            {
                var cacheOption = new RouteCacheOption(globalConfigurationId, routeName)
                {
                    TtlSeconds = input.TtlSeconds.Value,
                    Region = input.Region
                };

                return cacheOption;
            }

            return null;
        }

        public override async Task<OcelotDto> UpdateAsync(Guid id, OcelotUpdateDto input)
        {
            var entity = await GetEntityByIdAsync(id).ConfigureAwait(false);

            MapToEntity(input, entity);
            await OcelotRepository.UpdateAsync(entity).ConfigureAwait(false);

            return MapToGetOutputDto(entity);
        }

        public override async Task<PagedResultDto<OcelotDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await base.GetListAsync(input);
        }

        [Authorize(AbpOcelotManagementPermissions.Ocelots.Create)]
        public override async Task<OcelotDto> CreateAsync(OcelotCreateDto input)
        {
            var ocelot = await OcelotManager.CreateAsync(input.Name);
            await OcelotRepository.InsertAsync(ocelot);

            return ObjectMapper.Map<Ocelot, OcelotDto>(ocelot);
        }

        [Authorize(AbpOcelotManagementPermissions.Ocelots.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        [AllowAnonymous]
        public async Task Reload(Guid id)
        {
            var ocelot = await base.GetEntityByIdAsync(id);
            await DistributedEventBus.PublishAsync(new UpdateFileConfigurationEventEventData(ocelot.Name, DateTime.Now));
        }
    }
}
