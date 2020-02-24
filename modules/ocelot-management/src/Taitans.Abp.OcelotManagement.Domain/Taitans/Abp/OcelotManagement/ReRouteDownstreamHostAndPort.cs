using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteDownstreamHostAndPort : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }

        public ReRouteDownstreamHostAndPort(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName, Host, Port };
        }
    }
}
