using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteDownstreamHostAndPort : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }

        public RouteDownstreamHostAndPort(Guid globalConfigurationId, string routeName, string host, int port)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;
            Host = host;
            Port = port;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName, Host, Port };
        }

        public bool Equals(Guid globalConfigurationId, string routeName, string host, int port)
        {
            return GlobalConfigurationId == globalConfigurationId && RouteName == routeName && Host == host && Port == port;
        } 
    }
}
