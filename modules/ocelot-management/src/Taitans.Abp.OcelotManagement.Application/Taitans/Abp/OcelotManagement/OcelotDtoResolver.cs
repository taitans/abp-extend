using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Taitans.Abp.OcelotManagement
{
    internal class ReRouteUpstreamHttpMethodResolver : IValueResolver<OcelotReRouteDto, OcelotReRoute, List<ReRouteUpstreamHttpMethod>>
    {
        public List<ReRouteUpstreamHttpMethod> Resolve(OcelotReRouteDto source, OcelotReRoute destination, List<ReRouteUpstreamHttpMethod> destMember, ResolutionContext context)
        {
            if (source == null || source.UpstreamHttpMethods == null)
            {
                return null;
            }

            foreach (var item in source.UpstreamHttpMethods)
            {
                destination.AddUpstreamHttpMethod(item);
            }

            return destination.UpstreamHttpMethods;
        }
    }

    internal class ReRouteUpstreamHttpMethodDtoResolver : IValueResolver<OcelotReRoute, OcelotReRouteDto, List<string>>
    {
        public List<string> Resolve(OcelotReRoute source, OcelotReRouteDto destination, List<string> destMember, ResolutionContext context)
        {
            if (source == null || source.UpstreamHttpMethods == null)
            {
                return null;
            }

            return source.UpstreamHttpMethods.Select(c => c.Method).ToList();
        }
    }

    internal class ReRouteDownstreamHostAndPortDtoResolver : IValueResolver<OcelotReRoute, OcelotReRouteDto, List<ReRouteDownstreamHostAndPortDto>>
    {
        public List<ReRouteDownstreamHostAndPortDto> Resolve(OcelotReRoute source, OcelotReRouteDto destination, List<ReRouteDownstreamHostAndPortDto> destMember, ResolutionContext context)
        {
            if (source == null || source.DownstreamHostAndPorts == null)
            {
                return null;
            }

            List<ReRouteDownstreamHostAndPortDto> dto = new List<ReRouteDownstreamHostAndPortDto>();
            foreach (var item in source.DownstreamHostAndPorts)
            {
                dto.Add(new ReRouteDownstreamHostAndPortDto { Host = item.Host, Port = item.Port });
            }

            return dto;
        }
    }

    internal class ReRouteDelegatingHandlerResolver : IValueResolver<OcelotReRouteDto, OcelotReRoute, List<ReRouteDelegatingHandler>>
    {
        public List<ReRouteDelegatingHandler> Resolve(OcelotReRouteDto source, OcelotReRoute destination, List<ReRouteDelegatingHandler> destMember, ResolutionContext context)
        {
            if (source == null || source.DelegatingHandlers == null)
            {
                return null;
            }
            foreach (var item in source.DelegatingHandlers)
            {
                destination.AddDelegatingHandler(item);
            }

            return destination.DelegatingHandlers;
        }
    }

    internal class ReRouteDelegatingHandlerDtoResolver : IValueResolver<OcelotReRoute, OcelotReRouteDto, List<string>>
    {
        public List<string> Resolve(OcelotReRoute source, OcelotReRouteDto destination, List<string> destMember, ResolutionContext context)
        {
            if (source == null || source.DelegatingHandlers == null)
            {
                return null;
            }

            return source.DelegatingHandlers.Select(c => c.Delegating).ToList();
        }
    }

    internal class ReRouteDownstreamHostAndPortResolver : IValueResolver<OcelotReRouteDto, OcelotReRoute, List<ReRouteDownstreamHostAndPort>>
    {
        public List<ReRouteDownstreamHostAndPort> Resolve(OcelotReRouteDto source, OcelotReRoute destination, List<ReRouteDownstreamHostAndPort> destMember, ResolutionContext context)
        {
            if (source == null || source.DownstreamHostAndPorts == null)
            {
                return null;
            }

            foreach (var item in source.DownstreamHostAndPorts)
            {
                destination.AddDownstreamHostAndPort(item.Host, item.Port.Value);
            }

            return destination.DownstreamHostAndPorts;
        }
    }

    internal class ReRouteDtoLoadBalancerOptionsResolver : IValueResolver<OcelotReRoute, OcelotReRouteDto, OcelotLoadBalancerOptionDto>
    {
        public OcelotLoadBalancerOptionDto Resolve(OcelotReRoute source, OcelotReRouteDto destination, OcelotLoadBalancerOptionDto destMember, ResolutionContext context)
        {
            if (source == null || source.LoadBalancerOption == null)
            {
                return null;
            }

            return new OcelotLoadBalancerOptionDto
            {
                Type = source.LoadBalancerOption.Type,
                Key = source.LoadBalancerOption.Key,
                Expiry = source.LoadBalancerOption.Expiry,
            };
        }
    }
}
