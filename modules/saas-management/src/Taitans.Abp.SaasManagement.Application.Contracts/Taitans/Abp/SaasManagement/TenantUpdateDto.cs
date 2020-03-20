using System;

namespace Taitans.Abp.SaasManagement
{
    public class TenantUpdateDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Guid? EditionId { get; set; }
    }
}
