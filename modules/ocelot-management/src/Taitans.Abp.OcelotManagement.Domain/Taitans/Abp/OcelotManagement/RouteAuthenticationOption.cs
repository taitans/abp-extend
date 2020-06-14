using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteAuthenticationOption : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string AuthenticationProviderKey { get; set; }
        public virtual List<RouteAuthenticationOptionAllowedScope> AllowedScopes { get; protected set; }

        public RouteAuthenticationOption(Guid globalConfigurationId, string routeName)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            AllowedScopes = new List<RouteAuthenticationOptionAllowedScope>();
        }

        public void AddScope(string scope)
        {
            AllowedScopes.Add(new RouteAuthenticationOptionAllowedScope(GlobalConfigurationId, RouteName, scope));
        }

        public virtual void RemoveAllScopes()
        {
            AllowedScopes.Clear();
        }

        public void RemoveScopes(string scope)
        {
            AllowedScopes.RemoveAll(c => c.Scope == scope);
        }

        public void FindScope(string scope)
        {
            AllowedScopes.FirstOrDefault(c => c.Scope == scope);
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName };
        }
    }
}
