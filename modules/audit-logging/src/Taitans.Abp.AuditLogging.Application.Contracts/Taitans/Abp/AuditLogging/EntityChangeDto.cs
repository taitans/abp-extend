using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace Taitans.Abp.AuditLogging
{
    public class EntityChangeDto : EntityDto<Guid>, IMultiTenant, IHasExtraProperties
    {
        public Guid AuditLogId { get; set; }
        public Guid? TenantId { get; set; }
        public DateTime ChangeTime { get; set; }
        public EntityChangeType ChangeType { get; set; }
        public Guid? EntityTenantId { get; set; }
        public string EntityId { get; set; }
        public string EntityTypeFullName { get; set; }
        public List<EntityPropertyChangeDto> PropertyChanges { get; set; }
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}