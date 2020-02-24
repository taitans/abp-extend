using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteUpstreamHttpMethod : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string Method { get; set; }

        protected internal ReRouteUpstreamHttpMethod(string method)
        {
            Method = method;
        }

        public override string ToString()
        {
            return Method;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName, Method };
        }
    }
}
