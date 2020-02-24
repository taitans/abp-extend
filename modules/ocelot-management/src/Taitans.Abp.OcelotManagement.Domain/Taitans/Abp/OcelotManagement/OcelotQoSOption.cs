using System;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotQoSOption : QoSOptionBase
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId };
        }
    }
}
