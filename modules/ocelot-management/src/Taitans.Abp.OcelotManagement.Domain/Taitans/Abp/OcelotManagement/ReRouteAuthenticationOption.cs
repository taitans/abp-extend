using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteAuthenticationOption : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string AuthenticationProviderKey { get; set; }
        public virtual List<ReRouteAuthenticationOptionAllowedScope> AllowedScopes { get; protected set; }

        public ReRouteAuthenticationOption()
        {
            AllowedScopes = new List<ReRouteAuthenticationOptionAllowedScope>();
        }

        public void AddScope(string scope)
        {
            AllowedScopes.Add(new ReRouteAuthenticationOptionAllowedScope(scope));
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
            return new object[] { GlobalConfigurationId, ReRouteName };
        }
    }
}
