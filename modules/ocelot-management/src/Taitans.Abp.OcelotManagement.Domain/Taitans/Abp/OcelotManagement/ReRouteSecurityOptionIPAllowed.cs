namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteSecurityOptionIPAllowed : ReRouteSecurityOptionIPBase
    {
        protected internal ReRouteSecurityOptionIPAllowed()
        {

        }

        public ReRouteSecurityOptionIPAllowed(string ip)
        {
            IP = ip;
        }

        public override string ToString()
        {
            return IP;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName, IP };
        }
    }
}