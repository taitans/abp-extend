using System;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotHttpHandlerOption : HttpHandlerOptionBase
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }

        public OcelotHttpHandlerOption(Guid globalConfigurationId)
        {
            GlobalConfigurationId = globalConfigurationId;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId };
        }
    }
}
