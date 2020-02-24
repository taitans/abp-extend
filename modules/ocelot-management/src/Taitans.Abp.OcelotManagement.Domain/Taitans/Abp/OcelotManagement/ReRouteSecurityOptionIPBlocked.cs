namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteSecurityOptionIPBlocked : ReRouteSecurityOptionIPBase
    {
        protected internal ReRouteSecurityOptionIPBlocked()
        {

        }

        public ReRouteSecurityOptionIPBlocked(string ip)
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