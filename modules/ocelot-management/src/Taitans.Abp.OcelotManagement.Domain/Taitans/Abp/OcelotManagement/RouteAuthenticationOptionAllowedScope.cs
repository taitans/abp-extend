using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteAuthenticationOptionAllowedScope : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string Scope { get; set; }

        protected internal RouteAuthenticationOptionAllowedScope() { }

        public RouteAuthenticationOptionAllowedScope(Guid globalConfigurationId, string routeName, string scope)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            Scope = scope;
        }

        public override string ToString()
        {
            return Scope;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName, Scope };
        }
    }
}