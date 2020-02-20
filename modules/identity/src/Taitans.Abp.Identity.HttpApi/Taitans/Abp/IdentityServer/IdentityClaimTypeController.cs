using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taitans.Abp.Identity;
using Volo.Abp;

namespace Taitans.Abp.IdentityServer
{
    [RemoteService]
    [Area("IdentityServer")]
    [ControllerName("ClaimTypes")]
    [Route("/api/identity-server/claim-types")]
    public class IdentityClaimTypeController : IdentityController
    {
        private readonly IIdentityClaimTypeAppService _identityClaimTypeAppService;

        public IdentityClaimTypeController(IIdentityClaimTypeAppService identityClaimTypeAppService)
        {
            _identityClaimTypeAppService = identityClaimTypeAppService;
        }

        [HttpGet]
        public Task<List<string>> GetClaimTypes()
        {
            return _identityClaimTypeAppService.GetClaimTypes();
        }
    }
}
