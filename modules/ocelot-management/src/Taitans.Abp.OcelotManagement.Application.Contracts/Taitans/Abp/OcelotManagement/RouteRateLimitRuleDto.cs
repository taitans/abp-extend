using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteRateLimitRuleDto
    {
        public virtual List<string> ClientWhitelist { get; set; }
        public virtual bool? EnableRateLimiting { get; set; }
        public virtual string Period { get; set; }
        public virtual double? PeriodTimespan { get; set; }
        public virtual long? Limit { get; set; }
    }
}