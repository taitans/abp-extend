using System;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteLoadBalancerOption : LoadBalancerOptionBase
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }

        public RouteLoadBalancerOption(Guid globalConfigurationId, string routeName)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName };
        }
    }
}
