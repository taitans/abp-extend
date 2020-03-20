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
    [ControllerName("Tenant")]
    [Route("/api/saas/tenants")]
    [Authorize]
    public class TenantController : SaasManagementControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantController(ITenantAppService saasTenantAppService)
        {
            _tenantAppService = saasTenantAppService;
        }

        [HttpPost]
        public async Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return await _tenantAppService.CreateAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _tenantAppService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<TenantDto> GetAsync(Guid id)
        {
            return await _tenantAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantInput input)
        {
            return await _tenantAppService.GetListAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<TenantDto> UpdateAsync(Guid id, TenantUpdateDto input)
        {
            return await _tenantAppService.UpdateAsync(id, input);
        }

        [HttpGet("{id}/default-connection-string")]
        public async Task<string> GetDefaultConnectionStringAsync(Guid id)
        {
            return await _tenantAppService.GetDefaultConnectionStringAsync(id);
        }
        [HttpDelete("{id}/default-connection-string")]
        public async Task DeleteDefaultConnectionStringAsync(Guid id)
        {
            await _tenantAppService.DeleteDefaultConnectionStringAsync(id);
        }
        [HttpPut("{id}/default-connection-string")]
        public async Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString)
        {
            await _tenantAppService.UpdateDefaultConnectionStringAsync(id, defaultConnectionString);
        }
    }
}
