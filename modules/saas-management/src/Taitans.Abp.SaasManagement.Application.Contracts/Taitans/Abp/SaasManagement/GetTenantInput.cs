using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.SaasManagement
{
    public class GetTenantInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
