using System;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteSecurityOptionIPAllowed : RouteSecurityOptionIPBase
    {
        protected RouteSecurityOptionIPAllowed()
        {

        }

        public RouteSecurityOptionIPAllowed(Guid globalConfigurationId, string routeName, string ip) 
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