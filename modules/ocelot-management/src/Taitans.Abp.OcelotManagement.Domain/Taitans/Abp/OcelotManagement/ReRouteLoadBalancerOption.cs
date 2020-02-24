using System;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteLoadBalancerOption : LoadBalancerOptionBase
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName };
        }
    }
}
