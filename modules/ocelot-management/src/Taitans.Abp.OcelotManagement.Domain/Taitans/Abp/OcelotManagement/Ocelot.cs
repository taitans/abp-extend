using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Taitans.Abp.OcelotManagement
{
    public class Ocelot : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }
        public virtual string RequestIdKey { get; set; }
        public virtual OcelotServiceDiscoveryProvider ServiceDiscoveryProvider { get; set; }
        public virtual OcelotRateLimitOption RateLimitOption { get; set; }
        public virtual OcelotQoSOption QoSOption { get; set; }
        public virtual string BaseUrl { get; set; }
        public virtual OcelotLoadBalancerOption LoadBalancerOption { get; set; }
        public virtual string DownstreamScheme { get; set; }
        public virtual OcelotHttpHandlerOption HttpHandlerOption { get; set; }
        public virtual List<OcelotRoute> Routes { get; protected set; }

        protected Ocelot() { }

        public Ocelot(Guid id, string name, string requestIdKey = null, string baseUrl = null, string downstreamScheme = "http")
        {
            Check.NotNull(name, nameof(name));

            Id = id;

            Name = name;
            RequestIdKey = requestIdKey;
            BaseUrl = baseUrl;
            DownstreamScheme = downstreamScheme;
            Routes = new List<OcelotRoute>();
        }

        public virtual void AddRoutes(string name,
            string upstreamPathTemplate,
            string upstreamHost,
            string downstreamHttpMethod,
            string downstreamPathTemplate,
            string downstreamScheme,
            string key = null,
            string serviceNamespace = null,
            string serviceName = null,
            bool routeIsCaseSensitive = false,
            string requestIdKey = null,
            bool dangerousAcceptAnyServerCertificateValidator = false,
            int timeout = 5000,
            int sort = 100,
            int priority = 1,
            List<string> upstreamHttpMethods = null,
            Dictionary<string, int> downstreamHostAndPorts = null)
        {
            var route = new OcelotRoute(
                 Id,
                 name,
                 upstreamHost,
                 upstreamPathTemplate,
                 downstreamHttpMethod,
                 downstreamPathTemplate,
                 downstreamScheme,
                 key,
                 serviceNamespace,
                 serviceName,
                 routeIsCaseSensitive,
                 requestIdKey,
                 dangerousAcceptAnyServerCertificateValidator,
                 timeout,
                 sort,
                 priority
            );
            if (upstreamHttpMethods != null)
            {
                foreach (var item in upstreamHttpMethods)
                {
                    route.AddUpstreamHttpMethod(item);
                }

            }
            if (downstreamHostAndPorts != null)
            {
                foreach (var item in downstreamHostAndPorts)
                {
                    route.AddDownstreamHostAndPort(item.Key, item.Value);
                }
            }

            Routes.Add(route);

        }

        public virtual void RemoveAllRoutes()
        {
            foreach (var route in Routes)
            {
                route.RemoveAllDownstreamHostAndPorts();
                route.RemoveAllUpstreamHttpMethods();
            }
            Routes.Clear();
        }

        public virtual void RemoveRoute(string name)
        {
            Routes.RemoveAll(c => c.Name == name);
        }

        public virtual OcelotRoute FindRoute(string name)
        {
            return Routes.FirstOrDefault(c => c.Name == name);
        }
    }
}
