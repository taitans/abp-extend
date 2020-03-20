using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.SaasManagement
{
    public class GetEditionInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
