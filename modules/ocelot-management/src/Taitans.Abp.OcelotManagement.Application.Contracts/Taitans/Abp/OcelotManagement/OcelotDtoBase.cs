using System;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotDtoBase : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string RequestIdKey { get; set; }
        public OcelotServiceDiscoveryProviderDto ServiceDiscoveryProvider { get; set; }
        public OcelotRateLimitOptionDto RateLimitOption { get; set; }
        public OcelotQoSOptionDto QoSOption { get; set; }
        public string BaseUrl { get; set; }
        public OcelotLoadBalancerOptionDto LoadBalancerOption { get; set; }
        public string DownstreamScheme { get; set; }
        public OcelotHttpHandlerOptionDto HttpHandlerOption { get; set; }
    }
}
