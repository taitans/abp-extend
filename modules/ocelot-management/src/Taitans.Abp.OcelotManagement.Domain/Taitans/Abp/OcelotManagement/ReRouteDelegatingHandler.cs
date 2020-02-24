using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteDelegatingHandler : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string Delegating { get; set; }

        public ReRouteDelegatingHandler(string delegating)
        {
            Delegating = delegating;
        }

        public override string ToString()
        {
            return Delegating;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName, Delegating };
        }
    }
}
