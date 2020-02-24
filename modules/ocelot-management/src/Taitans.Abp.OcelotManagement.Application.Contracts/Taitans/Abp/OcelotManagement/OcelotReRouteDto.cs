using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotReRouteDto
    {
        public string Name { get; set; }

        public int Timeout { get; set; }
        public int Priority { get; set; }        public List<string> DelegatingHandlers { get; set; }        public string Key { get; set; }        public string UpstreamHost { get; set; }
        public List<ReRouteDownstreamHostAndPortDto> DownstreamHostAndPorts { get; set; }        public ReRouteHttpHandlerOptionDto HttpHandlerOption { get; set; }        public ReRouteAuthenticationOptionDto AuthenticationOption { get; set; }        public ReRouteRateLimitRuleDto RateLimitOption { get; set; }
        public ReRouteLoadBalancerOptionDto LoadBalancerOption { get; set; }        public ReRouteQoSOptionDto QoSOption { get; set; }        public string DownstreamScheme { get; set; }        public string ServiceName { get; set; }        public bool? ReRouteIsCaseSensitive { get; set; }        public ReRouteCacheOptionDto CacheOption { get; set; }        public string RequestIdKey { get; set; }
        public Dictionary<string, string> AddQueriesToRequests { get; set; }        public Dictionary<string, string> RouteClaimsRequirements { get; set; }        public Dictionary<string, string> AddClaimsToRequests { get; set; }        public Dictionary<string, string> DownstreamHeaderTransforms { get; set; }        public Dictionary<string, string> UpstreamHeaderTransforms { get; set; }        public Dictionary<string, string> AddHeadersToRequests { get; set; }
        public Dictionary<string, string> ChangeDownstreamPathTemplates { get; set; }
        public List<string> UpstreamHttpMethods { get; set; }        public string UpstreamPathTemplate { get; set; }        public string DownstreamPathTemplate { get; set; }        public bool? DangerousAcceptAnyServerCertificateValidator { get; set; }        public ReRouteSecurityOptionDto SecurityOption { get; set; }
        public int? Sort { get; set; }
        public string DownstreamHttpMethod { get; set; }
        public string ServiceNamespace { get; set; }
    }
}