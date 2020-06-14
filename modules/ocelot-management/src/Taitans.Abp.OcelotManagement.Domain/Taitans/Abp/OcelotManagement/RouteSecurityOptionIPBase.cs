using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class RouteSecurityOptionIPBase : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string IP { get; set; }

        public virtual bool Equals(Guid globalConfigurationId, string routeName, string ip)
        {
            return GlobalConfigurationId == globalConfigurationId && RouteName == routeName && IP == ip; 
        }
    }
}
