using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class HttpHandlerOptionBase : Entity
    {
        public virtual bool AllowAutoRedirect { get; set; }
        public virtual bool UseCookieContainer { get; set; }
        public virtual bool UseTracing { get; set; }
        public virtual bool UseProxy { get; set; }
        public virtual int MaxConnectionsPerServer { get; set; }
    }
}
