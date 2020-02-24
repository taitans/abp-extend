using System;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotLoadBalancerOption : LoadBalancerOptionBase
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId };
        }
    }
}
