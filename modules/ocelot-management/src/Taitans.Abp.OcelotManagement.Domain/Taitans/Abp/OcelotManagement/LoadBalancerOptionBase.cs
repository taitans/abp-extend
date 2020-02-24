using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class LoadBalancerOptionBase : Entity
    {
        public virtual string Type { get; set; }
        public virtual string Key { get; set; }
        public virtual int Expiry { get; set; }
    }
}
