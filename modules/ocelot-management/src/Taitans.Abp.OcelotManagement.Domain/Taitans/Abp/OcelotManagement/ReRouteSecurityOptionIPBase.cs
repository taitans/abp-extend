using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class ReRouteSecurityOptionIPBase : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string IP { get; set; }
    }
}
