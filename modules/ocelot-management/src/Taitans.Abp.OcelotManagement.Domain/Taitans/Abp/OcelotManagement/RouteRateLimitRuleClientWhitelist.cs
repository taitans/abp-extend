using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteRateLimitRuleClientWhitelist : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string Whitelist { get; set; }

        public RouteRateLimitRuleClientWhitelist(Guid globalConfigurationId, string routeName, string whitelist)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            Whitelist = whitelist;
        }

        public override string ToString()
        {
            return Whitelist;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName, Whitelist };
        }
    }
}