using System;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.SaasManagement
{

    public class TenantDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Guid? EditionId { get; set; }
        public string EditionName { get; set; }
    }
}
