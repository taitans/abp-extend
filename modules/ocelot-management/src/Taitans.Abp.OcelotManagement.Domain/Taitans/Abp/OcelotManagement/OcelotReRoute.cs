using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotReRoute : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual int Timeout { get; set; }
        public virtual int Priority { get; set; }        public virtual List<ReRouteDelegatingHandler> DelegatingHandlers { get; protected set; }        public virtual string Key { get; set; }        public virtual string UpstreamHost { get; set; }
        public virtual List<ReRouteDownstreamHostAndPort> DownstreamHostAndPorts { get; protected set; }        public virtual ReRouteHttpHandlerOption HttpHandlerOption { get; set; }        public virtual ReRouteAuthenticationOption AuthenticationOption { get; set; }        public virtual ReRouteRateLimitRule RateLimitOption { get; set; }
        public virtual ReRouteLoadBalancerOption LoadBalancerOption { get; set; }        public virtual ReRouteQoSOption QoSOption { get; set; }        public virtual string DownstreamScheme { get; set; }        public virtual string ServiceName { get; set; }        public virtual bool ReRouteIsCaseSensitive { get; set; }        public virtual ReRouteCacheOption CacheOption { get; set; }        public virtual string RequestIdKey { get; set; }
        public virtual Dictionary<string, string> AddQueriesToRequests { get; set; }        public virtual Dictionary<string, string> RouteClaimsRequirements { get; set; }        public virtual Dictionary<string, string> AddClaimsToRequests { get; set; }        public virtual Dictionary<string, string> DownstreamHeaderTransforms { get; set; }        public virtual Dictionary<string, string> UpstreamHeaderTransforms { get; set; }        public virtual Dictionary<string, string> AddHeadersToRequests { get; set; }
        public virtual Dictionary<string, string> ChangeDownstreamPathTemplates { get; set; }
        public virtual List<ReRouteUpstreamHttpMethod> UpstreamHttpMethods { get; protected set; }        public virtual string UpstreamPathTemplate { get; set; }        public virtual string DownstreamPathTemplate { get; set; }        public virtual bool DangerousAcceptAnyServerCertificateValidator { get; set; }        public virtual ReRouteSecurityOption SecurityOption { get; set; }
        public virtual int Sort { get; set; }
        public virtual string DownstreamHttpMethod { get; set; }
        public virtual string ServiceNamespace { get; set; }

        protected internal OcelotReRoute() { }

        public OcelotReRoute(Guid globalConfigurationId,
            string name,
            string upstreamPathTemplate,
            string upstreamHost,
            string downstreamScheme,
            string downstreamPathTemplate)
        {
            Check.NotNull(name, nameof(name));

            GlobalConfigurationId = globalConfigurationId;
            Name = name;
            UpstreamPathTemplate = upstreamPathTemplate;
            UpstreamHost = upstreamHost;
            DownstreamScheme = downstreamScheme;
            DownstreamPathTemplate = downstreamPathTemplate;

            UpstreamHttpMethods = new List<ReRouteUpstreamHttpMethod>();
            DownstreamHostAndPorts = new List<ReRouteDownstreamHostAndPort>();
            DelegatingHandlers = new List<ReRouteDelegatingHandler>();

            Sort = 100;

            AddHeadersToRequests = new Dictionary<string, string>();
            AddClaimsToRequests = new Dictionary<string, string>();
            RouteClaimsRequirements = new Dictionary<string, string>();
            AddQueriesToRequests = new Dictionary<string, string>();
            ChangeDownstreamPathTemplates = new Dictionary<string, string>();
            DownstreamHeaderTransforms = new Dictionary<string, string>();
            UpstreamHeaderTransforms = new Dictionary<string, string>();

            Priority = 1;

            //CacheOption = new ReRouteCacheOption();
            //QoSOption = new ReRouteQoSOption();
            //RateLimitOption = new ReRouteRateLimitRule();
            //AuthenticationOption = new ReRouteAuthenticationOption();
            //HttpHandlerOption  = new ReRouteHttpHandlerOption();
            //LoadBalancerOption = new ReRouteLoadBalancerOption();
            //SecurityOption = new ReRouteSecurityOption();
        }

        public virtual void AddDelegatingHandler(string delegating)
        {
            DelegatingHandlers.Add(new ReRouteDelegatingHandler(delegating));
        }

        public virtual void RemoveAllDelegatingHandlers()
        {
            DelegatingHandlers.Clear();
        }

        public void RemoveDelegatingHandlers(string delegating)
        {
            DelegatingHandlers.RemoveAll(c => c.Delegating == delegating);
        }

        public ReRouteDelegatingHandler FindDelegatingHandler(string delegating)
        {
            return DelegatingHandlers.FirstOrDefault(c => c.Delegating == delegating);
        }

        public virtual void AddUpstreamHttpMethod(string method)
        {
            UpstreamHttpMethods.Add(new ReRouteUpstreamHttpMethod(method));
        }

        public virtual void RemoveAllUpstreamHttpMethods()
        {
            UpstreamHttpMethods.Clear();
        }

        public virtual void RemoveUpstreamHttpMethod(string method)
        {
            UpstreamHttpMethods.RemoveAll(c => c.Method == method);
        }

        public virtual ReRouteUpstreamHttpMethod FindUpstreamHttpMethod(string method)
        {
            return UpstreamHttpMethods.FirstOrDefault(c => c.Method == method);
        }

        public virtual void AddDownstreamHostAndPort(string host, int port)
        {
            DownstreamHostAndPorts.Add(new ReRouteDownstreamHostAndPort(host, port));
        }

        public virtual void RemoveAllDownstreamHostAndPorts()
        {
            DownstreamHostAndPorts.Clear();
        }

        public virtual void RemoveDownstreamHostAndPort(string host, int port)
        {
            DownstreamHostAndPorts.RemoveAll(c => c.Host == host && c.Port == port);
        }

        public virtual ReRouteDownstreamHostAndPort FindDownstreamHostAndPort(string host, int port)
        {
            return DownstreamHostAndPorts.FirstOrDefault(c => c.Host == host && c.Port == port);
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, Name };
        }
    }
}
