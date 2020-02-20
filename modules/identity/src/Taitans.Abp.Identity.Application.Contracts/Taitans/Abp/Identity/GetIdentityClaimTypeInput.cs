using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.Identity
{
    public class GetIdentityClaimTypeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
