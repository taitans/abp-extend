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
        public virtual List<OcelotReRoute> ReRoutes { get; protected set; }

        protected Ocelot() { }

        public Ocelot(Guid id, string name, string requestIdKey = null, string baseUrl = null, string downstreamScheme = "http")
        {
            Check.NotNull(name, nameof(name));

            Id = id;

            Name = name;
            RequestIdKey = requestIdKey;
            BaseUrl = baseUrl;
            DownstreamScheme = downstreamScheme;
            ReRoutes = new List<OcelotReRoute>();
        }

        public virtual void AddReRoutes(string name,
            string upstreamPathTemplate,
            string upstreamHost,
            string downstreamScheme,
            string downstreamPathTemplate,
            List<string> upstreamHttpMethods = null,
            Dictionary<string, int> downstreamHostAndPorts = null)
        {
            var reRoute = new OcelotReRoute(
                 Id,
                 name,
                 upstreamPathTemplate,
                 upstreamHost,
                 downstreamScheme,
                 downstreamPathTemplate
            );
            if (upstreamHttpMethods != null)
            {
                foreach (var item in upstreamHttpMethods)
                {
                    reRoute.AddUpstreamHttpMethod(item);
                }

            }
            if (downstreamHostAndPorts != null)
            {
                foreach (var item in downstreamHostAndPorts)
                {
                    reRoute.AddDownstreamHostAndPort(item.Key, item.Value);
                }
            }

            ReRoutes.Add(reRoute);

        }

        public virtual void RemoveAllReRoutes()
        {
            foreach (var route in ReRoutes)
            {
                route.RemoveAllDownstreamHostAndPorts();
                route.RemoveAllUpstreamHttpMethods();
            }
            ReRoutes.Clear();
        }

        public virtual void RemoveReRoute(string name)
        {
            ReRoutes.RemoveAll(c => c.Name == name);
        }

        public virtual OcelotReRoute FindReRoute(string name)
        {
            return ReRoutes.FirstOrDefault(c => c.Name == name);
        }
    }
}
