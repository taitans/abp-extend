using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteRateLimitRuleClientWhitelist : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string Whitelist { get; set; }

        public ReRouteRateLimitRuleClientWhitelist(string whitelist)
        {
            Whitelist = whitelist;
        }

        public override string ToString()
        {
            return Whitelist;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName, Whitelist };
        }
    }
}