using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Taitans.Abp.OcelotManagement
{
    internal class RouteUpstreamHttpMethodResolver : IValueResolver<OcelotRouteDto, OcelotRoute, List<RouteUpstreamHttpMethod>>
    {
        public List<RouteUpstreamHttpMethod> Resolve(OcelotRouteDto source, OcelotRoute destination, List<RouteUpstreamHttpMethod> destMember, ResolutionContext context)
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

    internal class RouteUpstreamHttpMethodDtoResolver : IValueResolver<OcelotRoute, OcelotRouteDto, List<string>>
    {
        public List<string> Resolve(OcelotRoute source, OcelotRouteDto destination, List<string> destMember, ResolutionContext context)
        {
            if (source == null || source.UpstreamHttpMethods == null)
            {
                return null;
            }

            return source.UpstreamHttpMethods.Select(c => c.Method).ToList();
        }
    }

    internal class RouteDownstreamHostAndPortDtoResolver : IValueResolver<OcelotRoute, OcelotRouteDto, List<RouteDownstreamHostAndPortDto>>
    {
        public List<RouteDownstreamHostAndPortDto> Resolve(OcelotRoute source, OcelotRouteDto destination, List<RouteDownstreamHostAndPortDto> destMember, ResolutionContext context)
        {
            if (source == null || source.DownstreamHostAndPorts == null)
            {
                return null;
            }

            List<RouteDownstreamHostAndPortDto> dto = new List<RouteDownstreamHostAndPortDto>();
            foreach (var item in source.DownstreamHostAndPorts)
            {
                dto.Add(new RouteDownstreamHostAndPortDto { Host = item.Host, Port = item.Port });
            }

            return dto;
        }
    }

    internal class RouteDelegatingHandlerResolver : IValueResolver<OcelotRouteDto, OcelotRoute, List<RouteDelegatingHandler>>
    {
        public List<RouteDelegatingHandler> Resolve(OcelotRouteDto source, OcelotRoute destination, List<RouteDelegatingHandler> destMember, ResolutionContext context)
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

    internal class RouteDelegatingHandlerDtoResolver : IValueResolver<OcelotRoute, OcelotRouteDto, List<string>>
    {
        public List<string> Resolve(OcelotRoute source, OcelotRouteDto destination, List<string> destMember, ResolutionContext context)
        {
            if (source == null || source.DelegatingHandlers == null)
            {
                return null;
            }

            return source.DelegatingHandlers.Select(c => c.Delegating).ToList();
        }
    }

    internal class RouteDownstreamHostAndPortResolver : IValueResolver<OcelotRouteDto, OcelotRoute, List<RouteDownstreamHostAndPort>>
    {
        public List<RouteDownstreamHostAndPort> Resolve(OcelotRouteDto source, OcelotRoute destination, List<RouteDownstreamHostAndPort> destMember, ResolutionContext context)
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

    internal class RouteDtoLoadBalancerOptionsResolver : IValueResolver<OcelotRoute, OcelotRouteDto, OcelotLoadBalancerOptionDto>
    {
        public OcelotLoadBalancerOptionDto Resolve(OcelotRoute source, OcelotRouteDto destination, OcelotLoadBalancerOptionDto destMember, ResolutionContext context)
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
