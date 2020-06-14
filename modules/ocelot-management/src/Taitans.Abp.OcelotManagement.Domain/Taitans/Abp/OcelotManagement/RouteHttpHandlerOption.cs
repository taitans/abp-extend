using System;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteHttpHandlerOption : HttpHandlerOptionBase
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }

        public RouteHttpHandlerOption(Guid globalConfigurationId, string routeName)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            MaxConnectionsPerServer = int.MaxValue;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName };
        }
    }
}
