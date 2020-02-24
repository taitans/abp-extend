namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotCreateOrUpdateDtoBase
    {
        public string RequestIdKey { get; set; }
        public string BaseUrl { get; set; }
        public string DownstreamScheme { get; set; }
        public OcelotLoadBalancerOptionDto LoadBalancerOptions { get; set; }
        public OcelotHttpHandlerOptionDto HttpHandlerOptions { get; set; }
    }
}
