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

            CreateMap<OcelotReRouteDto, OcelotReRoute>()
                .ConstructUsing(sourc => new OcelotReRoute(new Guid(), sourc.Name, sourc.UpstreamPathTemplate, sourc.UpstreamHost, sourc.DownstreamScheme, sourc.DownstreamPathTemplate))
                .ForMember(dest => dest.DelegatingHandlers, sourc => sourc.MapFrom(new ReRouteDelegatingHandlerResolver()))
                .ForMember(dest => dest.DownstreamHostAndPorts, sourc => sourc.MapFrom(new ReRouteDownstreamHostAndPortResolver()))
                .ForMember(dest => dest.UpstreamHttpMethods, sourc => sourc.MapFrom(new ReRouteUpstreamHttpMethodResolver()));
            CreateMap<OcelotReRoute, OcelotReRouteDto>()
                .ForMember(dest => dest.DelegatingHandlers, sourc => sourc.MapFrom(new ReRouteDelegatingHandlerDtoResolver()))
                .ForMember(dest => dest.UpstreamHttpMethods, sourc => sourc.MapFrom(new ReRouteUpstreamHttpMethodDtoResolver()))
                .ForMember(dest => dest.DownstreamHostAndPorts, sourc => sourc.MapFrom(new ReRouteDownstreamHostAndPortDtoResolver()));

            CreateMap<ReRouteHttpHandlerOptionDto, ReRouteHttpHandlerOption>();
            CreateMap<ReRouteHttpHandlerOption, ReRouteHttpHandlerOptionDto>();

            CreateMap<ReRouteAuthenticationOptionDto, ReRouteAuthenticationOption>();
            CreateMap<ReRouteAuthenticationOption, ReRouteAuthenticationOptionDto>();

            CreateMap<ReRouteRateLimitRuleDto, ReRouteRateLimitRule>();
            CreateMap<ReRouteRateLimitRule, ReRouteRateLimitRuleDto>();

            CreateMap<ReRouteLoadBalancerOptionDto, ReRouteLoadBalancerOption>();
            CreateMap<ReRouteLoadBalancerOption, ReRouteLoadBalancerOptionDto>();

            CreateMap<ReRouteQoSOptionDto, ReRouteQoSOption>();
            CreateMap<ReRouteQoSOption, ReRouteQoSOptionDto>();

            CreateMap<ReRouteCacheOptionDto, ReRouteCacheOption>();
            CreateMap<ReRouteCacheOption, ReRouteCacheOptionDto>();

            CreateMap<ReRouteSecurityOptionDto, ReRouteSecurityOption>();
            CreateMap<ReRouteSecurityOption, ReRouteSecurityOptionDto>();
        }
    }


}
