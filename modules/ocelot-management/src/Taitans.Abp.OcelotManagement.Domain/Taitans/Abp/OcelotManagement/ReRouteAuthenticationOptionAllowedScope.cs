using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteAuthenticationOptionAllowedScope : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string Scope { get; set; }

        protected internal ReRouteAuthenticationOptionAllowedScope() { }

        public ReRouteAuthenticationOptionAllowedScope(string scope)
        {
            Scope = scope;
        }

        public override string ToString()
        {
            return Scope;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName, Scope };
        }
    }
}