using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.OcelotManagement
{
    [RemoteService]
    [Area("Ocelot")]
    [ControllerName("Ocelot")]
    [Route("/api/ocelot/global-configs")]
    public class OcelotGlobalController : OcelotController, IOcelotAppService
    {
        private readonly IOcelotAppService _ocelotGlobalConfigurationService;

        public OcelotGlobalController(IOcelotAppService ocelotGlobalConfigurationService)
        {
            _ocelotGlobalConfigurationService = ocelotGlobalConfigurationService;
        }

        [HttpPost]
        public async Task<OcelotDto> CreateAsync(OcelotCreateDto input)
        {
            return await _ocelotGlobalConfigurationService.CreateAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _ocelotGlobalConfigurationService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<OcelotDto> GetAsync(Guid id)
        {
            return await _ocelotGlobalConfigurationService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<OcelotDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await _ocelotGlobalConfigurationService.GetListAsync(input);
        }

        [HttpGet("{id}/re-routes")]
        public async Task<IList<OcelotReRouteDto>> GetReRoutesAsync(Guid id)
        {
            return await _ocelotGlobalConfigurationService.GetReRoutesAsync(id);
        }

        [HttpPut("{id}/re-routes")]
        public async Task<IList<OcelotReRouteDto>> UpdateReRoutesAsync(Guid id, List<OcelotReRouteDto> input)
        {
            return await _ocelotGlobalConfigurationService.UpdateReRoutesAsync(id, input);
        }

        [HttpPut("{id}/update-gateway-routes")]
        public async Task UpdateGatewayRoutesAsync(Guid id)
        {
            await _ocelotGlobalConfigurationService.UpdateGatewayRoutesAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<OcelotDto> UpdateAsync(Guid id, OcelotUpdateDto input)
        {
            return await _ocelotGlobalConfigurationService.UpdateAsync(id, input);
        }
    }
}
