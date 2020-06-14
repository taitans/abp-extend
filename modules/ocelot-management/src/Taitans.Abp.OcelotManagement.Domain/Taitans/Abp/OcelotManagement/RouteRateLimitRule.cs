using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteRateLimitRule : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }

        public virtual List<RouteRateLimitRuleClientWhitelist> ClientWhitelist { get; protected set; }
        public virtual bool EnableRateLimiting { get; set; }
        public virtual string Period { get; set; }
        public virtual double PeriodTimespan { get; set; }
        public virtual long Limit { get; set; }

        public RouteRateLimitRule(Guid globalConfigurationId, string routeName)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            ClientWhitelist = new List<RouteRateLimitRuleClientWhitelist>();
        }

        public virtual void AddWhitelist(string whitelist)
        {
            ClientWhitelist.Add(new RouteRateLimitRuleClientWhitelist(GlobalConfigurationId, RouteName, whitelist));
        }

        public virtual void RemoveAllWhitelists()
        {
            ClientWhitelist.Clear();
        }

        public void RemoveWhitelists(string whitelist)
        {
            ClientWhitelist.RemoveAll(c => c.Whitelist == whitelist);
        }

        public void FindWhitelist(string whitelist)
        {
            ClientWhitelist.FirstOrDefault(c => c.Whitelist == whitelist);
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName };
        }
    }
}
