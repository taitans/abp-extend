using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Taitans.Abp.SaasManagement
{
    public class Edition : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string DisplayName { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Edition(Guid id, string displayName, Guid? tenantId)
        {
            Id = id;
            SetDisplayName(displayName);
            TenantId = tenantId;
        }

        internal void SetDisplayName(string displayName)
        {
            DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName), EditionConsts.MaxDisplayNameLength);
        }
    }
}
