using AutoMapper;
using System;

namespace Taitans.Abp.OcelotManagement
{
    public class AbpOcelotManagementApplicationAutoMapperProfile : Profile
    {
        public AbpOcelotManagementApplicationAutoMapperProfile()
        {
            CreateMap<OcelotDto, Ocelot>();
            CreateMap<Ocelot, OcelotDto>();

            CreateMap<OcelotCreateDto, Ocelot>();
            CreateMap<OcelotUpdateDto, Ocelot>();

            CreateMap<OcelotServiceDiscoveryProviderDto, OcelotServiceDiscoveryProvider>();
            CreateMap<OcelotServiceDiscoveryProvider, OcelotServiceDiscoveryProviderDto>();

            CreateMap<OcelotRateLimitOptionDto, OcelotRateLimitOption>();
            CreateMap<OcelotRateLimitOption, OcelotRateLimitOptionDto>();

            CreateMap<OcelotQoSOptionDto, OcelotQoSOption>();
            CreateMap<OcelotQoSOption, OcelotQoSOptionDto>();

            CreateMap<OcelotLoadBalancerOption, OcelotLoadBalancerOptionDto>();
            CreateMap<OcelotLoadBalancerOptionDto, OcelotLoadBalancerOption>();

            CreateMap<OcelotHttpHandlerOptionDto, OcelotHttpHandlerOption>();
            CreateMap<OcelotHttpHandlerOption, OcelotHttpHandlerOptionDto>();

            CreateMap<OcelotRoute, OcelotRouteDto>()
                .ForMember(dest => dest.DelegatingHandlers, sourc => sourc.MapFrom(new RouteDelegatingHandlerDtoResolver()))
                .ForMember(dest => dest.UpstreamHttpMethods, sourc => sourc.MapFrom(new RouteUpstreamHttpMethodDtoResolver()))
                .ForMember(dest => dest.DownstreamHostAndPorts, sourc => sourc.MapFrom(new RouteDownstreamHostAndPortDtoResolver()));

            CreateMap<RouteHttpHandlerOptionDto, RouteHttpHandlerOption>();
            CreateMap<RouteHttpHandlerOption, RouteHttpHandlerOptionDto>();

            CreateMap<RouteAuthenticationOptionDto, RouteAuthenticationOption>();
            CreateMap<RouteAuthenticationOption, RouteAuthenticationOptionDto>();

            CreateMap<RouteRateLimitRuleDto, RouteRateLimitRule>();
            CreateMap<RouteRateLimitRule, RouteRateLimitRuleDto>();

            CreateMap<RouteLoadBalancerOptionDto, RouteLoadBalancerOption>();
            CreateMap<RouteLoadBalancerOption, RouteLoadBalancerOptionDto>();

            CreateMap<RouteQoSOptionDto, RouteQoSOption>();
            CreateMap<RouteQoSOption, RouteQoSOptionDto>();

            CreateMap<RouteCacheOptionDto, RouteCacheOption>();
            CreateMap<RouteCacheOption, RouteCacheOptionDto>();

            CreateMap<RouteSecurityOptionDto, RouteSecurityOption>();
            CreateMap<RouteSecurityOption, RouteSecurityOptionDto>();
        }
    }


}
