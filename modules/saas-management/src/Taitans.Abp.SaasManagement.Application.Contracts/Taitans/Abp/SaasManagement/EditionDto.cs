using System;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.SaasManagement
{
    public class EditionDto : EntityDto<Guid>
    {
        public string DisplayName { get; set; }
    }
}
