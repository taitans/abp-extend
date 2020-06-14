using System;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteSecurityOptionIPBlocked : RouteSecurityOptionIPBase
    {
        protected RouteSecurityOptionIPBlocked()
        {

        }

        public RouteSecurityOptionIPBlocked(Guid globalConfigurationId, string routeName, string ip)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            IP = ip;
        }

        public override string ToString()
        {
            return IP;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName, IP };
        }
    }
}