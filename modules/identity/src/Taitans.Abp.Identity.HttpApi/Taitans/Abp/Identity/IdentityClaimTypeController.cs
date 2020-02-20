using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.Identity
{
    [RemoteService]
    [Area("identity")]
    [ControllerName("ClaimType")]
    [Route("/api/identity/claim-types")]
    public class IdentityClaimTypeController : IdentityController
    {
        private readonly IIdentityClaimTypeAppService _identityClaimTypeAppService;

        public IdentityClaimTypeController(IIdentityClaimTypeAppService identityClaimTypeAppService)
        {
            _identityClaimTypeAppService = identityClaimTypeAppService;
        }

        [HttpPost]
        public async Task<ClaimTypeDto> CreateAsync(CreateClaimTypeDto input)
        {
            return await _identityClaimTypeAppService.CreateAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _identityClaimTypeAppService.DeleteAsync(id);
        }

        [HttpGet("all")]
        public async Task<IList<ClaimTypeDto>> GetAll()
        {
            return await _identityClaimTypeAppService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ClaimTypeDto> GetAsync(Guid id)
        {
            return await _identityClaimTypeAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<ClaimTypeDto>> GetListAsync(GetIdentityClaimTypeInput input)
        {
            return await _identityClaimTypeAppService.GetListAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<ClaimTypeDto> UpdateAsync(Guid id, UpdateClaimTypeDto input)
        {
            return await _identityClaimTypeAppService.UpdateAsync(id, input);
        }
    }
}
