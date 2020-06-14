using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteUpstreamHttpMethod : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string Method { get; set; }

        protected internal RouteUpstreamHttpMethod(Guid globalConfigurationId , string routeName, string method)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            Method = method;
        }

        public override string ToString()
        {
            return Method;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName, Method };
        }

        public bool Equals(Guid globalConfigurationId, string name, string method)
        {
            return GlobalConfigurationId == globalConfigurationId && RouteName == name && Method == method;
        }
    }
}
