using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteDelegatingHandler : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string Delegating { get; set; }

        public RouteDelegatingHandler(Guid globalConfigurationId, string routeName, string delegating)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            Delegating = delegating;
        }

        public override string ToString()
        {
            return Delegating;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName, Delegating };
        }

        public bool Equals(Guid globalConfigurationId, string routeName, string delegating)
        {
            return GlobalConfigurationId == globalConfigurationId && RouteName == routeName && Delegating == delegating;
        }
    }
}
