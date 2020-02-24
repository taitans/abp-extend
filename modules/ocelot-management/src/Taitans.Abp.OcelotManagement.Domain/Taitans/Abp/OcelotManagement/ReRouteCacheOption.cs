using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteCacheOption : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual int TtlSeconds { get; set; }        public virtual string Region { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName };
        }
    }
}
