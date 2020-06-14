using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotRoute : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual int Timeout { get; set; }
        public virtual int Priority { get; set; }
        public virtual List<RouteDelegatingHandler> DelegatingHandlers { get; protected set; }
        public virtual string Key { get; set; }
        public virtual string UpstreamHost { get; set; }
        public virtual List<RouteDownstreamHostAndPort> DownstreamHostAndPorts { get; protected set; }
        public virtual RouteHttpHandlerOption HttpHandlerOption { get; set; }
        public virtual RouteAuthenticationOption AuthenticationOption { get; set; }
        public virtual RouteRateLimitRule RateLimitOption { get; set; }
        public virtual RouteLoadBalancerOption LoadBalancerOption { get; set; }
        public virtual RouteQoSOption QoSOption { get; set; }
        public virtual string DownstreamScheme { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual bool RouteIsCaseSensitive { get; set; }
        public virtual RouteCacheOption CacheOption { get; set; }
        public virtual string RequestIdKey { get; set; }
        public virtual Dictionary<string, string> AddQueriesToRequests { get; set; }
        public virtual Dictionary<string, string> RouteClaimsRequirements { get; set; }
        public virtual Dictionary<string, string> AddClaimsToRequests { get; set; }
        public virtual Dictionary<string, string> DownstreamHeaderTransforms { get; set; }
        public virtual Dictionary<string, string> UpstreamHeaderTransforms { get; set; }
        public virtual Dictionary<string, string> AddHeadersToRequests { get; set; }
        public virtual Dictionary<string, string> ChangeDownstreamPathTemplates { get; set; }
        public virtual List<RouteUpstreamHttpMethod> UpstreamHttpMethods { get; protected set; }
        public virtual string UpstreamPathTemplate { get; set; }
        public virtual string DownstreamPathTemplate { get; set; }
        public virtual bool DangerousAcceptAnyServerCertificateValidator { get; set; }
        public virtual RouteSecurityOption SecurityOption { get; set; }
        public virtual int Sort { get; set; }
        public virtual string DownstreamHttpMethod { get; set; }
        public virtual string ServiceNamespace { get; set; }

        protected internal OcelotRoute() { }

        public OcelotRoute(Guid globalConfigurationId,
            string name,
            string upstreamHost,
            string upstreamPathTemplate,
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
            int priority = 1)
        {
            Check.NotNull(name, nameof(name));

            GlobalConfigurationId = globalConfigurationId;
            Name = name;
            UpstreamPathTemplate = upstreamPathTemplate;
            UpstreamHost = upstreamHost;
            DownstreamScheme = downstreamScheme;
            DownstreamPathTemplate = downstreamPathTemplate;
            DownstreamHttpMethod = downstreamHttpMethod;

            UpstreamHttpMethods = new List<RouteUpstreamHttpMethod>();
            DownstreamHostAndPorts = new List<RouteDownstreamHostAndPort>();
            DelegatingHandlers = new List<RouteDelegatingHandler>();

            Sort = sort;

            AddHeadersToRequests = new Dictionary<string, string>();
            AddClaimsToRequests = new Dictionary<string, string>();
            RouteClaimsRequirements = new Dictionary<string, string>();
            AddQueriesToRequests = new Dictionary<string, string>();
            ChangeDownstreamPathTemplates = new Dictionary<string, string>();
            DownstreamHeaderTransforms = new Dictionary<string, string>();
            UpstreamHeaderTransforms = new Dictionary<string, string>();

            Priority = priority;

            Timeout = timeout;
            Key = key;
            ServiceNamespace = serviceNamespace;
            ServiceName = serviceName;
            RouteIsCaseSensitive = routeIsCaseSensitive;
            RequestIdKey = requestIdKey;
            DangerousAcceptAnyServerCertificateValidator = dangerousAcceptAnyServerCertificateValidator;

            //CacheOption = new RouteCacheOption();
            //QoSOption = new RouteQoSOption();
            //RateLimitOption = new RouteRateLimitRule();
            //AuthenticationOption = new RouteAuthenticationOption();
            //HttpHandlerOption  = new RouteHttpHandlerOption();
            //LoadBalancerOption = new RouteLoadBalancerOption();
            //SecurityOption = new RouteSecurityOption();
        }

        public virtual void AddDelegatingHandler(string delegating)
        {
            DelegatingHandlers.Add(new RouteDelegatingHandler(GlobalConfigurationId, Name, delegating));
        }

        public virtual void RemoveAllDelegatingHandlers()
        {
            DelegatingHandlers.Clear();
        }

        public void RemoveDelegatingHandlers(string delegating)
        {
            DelegatingHandlers.RemoveAll(c => c.Delegating == delegating);
        }

        public RouteDelegatingHandler FindDelegatingHandler(string delegating)
        {
            return DelegatingHandlers.FirstOrDefault(c => c.Delegating == delegating);
        }

        public virtual void AddUpstreamHttpMethod(string method)
        {
            UpstreamHttpMethods.Add(new RouteUpstreamHttpMethod(GlobalConfigurationId, Name, method));
        }

        public virtual void RemoveAllUpstreamHttpMethods()
        {
            UpstreamHttpMethods.Clear();
        }

        public virtual void RemoveUpstreamHttpMethod(string method)
        {
            UpstreamHttpMethods.RemoveAll(c => c.Method == method);
        }

        public virtual RouteUpstreamHttpMethod FindUpstreamHttpMethod(string method)
        {
            return UpstreamHttpMethods.FirstOrDefault(c => c.Method == method);
        }

        public virtual void AddDownstreamHostAndPort(string host, int port)
        {
            DownstreamHostAndPorts.Add(new RouteDownstreamHostAndPort(GlobalConfigurationId, Name, host, port));
        }

        public virtual void RemoveAllDownstreamHostAndPorts()
        {
            DownstreamHostAndPorts.Clear();
        }

        public virtual void RemoveDownstreamHostAndPort(string host, int port)
        {
            DownstreamHostAndPorts.RemoveAll(c => c.Host == host && c.Port == port);
        }

        public virtual RouteDownstreamHostAndPort FindDownstreamHostAndPort(string host, int port)
        {
            return DownstreamHostAndPorts.FirstOrDefault(c => c.Host == host && c.Port == port);
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, Name };
        }

        public virtual bool Equals(Guid globalConfigurationId, string name)
        {
            return GlobalConfigurationId == globalConfigurationId && Name == name;
        }
    }
}
