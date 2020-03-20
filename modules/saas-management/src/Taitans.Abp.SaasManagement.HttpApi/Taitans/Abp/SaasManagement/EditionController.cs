using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.SaasManagement
{
    [RemoteService]
    [Area("Saas")]
    [ControllerName("Edition")]
    [Route("/api/saas/editions")]
    [Authorize]
    public class EditionController : SaasManagementControllerBase, IEditionAppService
    {
        private readonly IEditionAppService _editionAppService;

        public EditionController(IEditionAppService editionAppService)
        {
            _editionAppService = editionAppService;
        }
        [HttpPost]
        public async Task<EditionDto> CreateAsync(EditionCreateDto input)
        {
            return await _editionAppService.CreateAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _editionAppService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<EditionDto> GetAsync(Guid id)
        {
            return await _editionAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<EditionDto>> GetListAsync(GetEditionInput input)
        {
            return await _editionAppService.GetListAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
        {
            return await _editionAppService.UpdateAsync(id, input);
        }
    }
}
