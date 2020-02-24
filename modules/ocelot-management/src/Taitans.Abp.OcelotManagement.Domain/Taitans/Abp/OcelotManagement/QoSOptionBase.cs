using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class QoSOptionBase : Entity
    {
        public virtual int ExceptionsAllowedBeforeBreaking { get; set; }
        public virtual int DurationOfBreak { get; set; }
        public virtual int TimeoutValue { get; set; }
    }
}
