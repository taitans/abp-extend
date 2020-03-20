using Microsoft.AspNetCore.Authorization;
using Taitans.Abp.SaasManagement.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;

namespace Taitans.Abp.SaasManagement
{
    [Authorize(AbpSaasManagementPermissions.Tenants.Default)]
    public class TenantAppService : CrudAppService<Tenant,
        TenantDto,
        Guid,
        GetTenantInput, TenantCreateDto, TenantUpdateDto>, ITenantAppService
    {
        public ITenantRepository SaasTenantRepository { get; }
        public ITenantManager SaasTenantManager { get; }
        public IEditionManager EditionManager { get; }
        public IDataSeeder DataSeeder { get; }

        public TenantAppService(ITenantRepository repository,
            ITenantManager saasTenantManager,
            IEditionManager editionManager,
            IDataSeeder dataSeeder) : base(repository)
        {
            SaasTenantRepository = repository;
            SaasTenantManager = saasTenantManager;
            EditionManager = editionManager;
            DataSeeder = dataSeeder;
        }

        public override async Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantInput input)
        {
            var count = await SaasTenantRepository.GetCountAsync(input.Filter);
            var list = await SaasTenantRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, true);

            return new PagedResultDto<TenantDto>(
                count,
                ObjectMapper.Map<List<Tenant>, List<TenantDto>>(list));
        }

        [Authorize(AbpSaasManagementPermissions.Tenants.Create)]
        public override async Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            var tenant = await SaasTenantManager.CreateAsync(input.Name, input.DisplayName, input.EditionId);
            await SaasTenantRepository.InsertAsync(tenant);

            using (CurrentTenant.Change(tenant.Id, tenant.Name))
            {
                //TODO: Handle database creation?

                //TODO: Set admin email & password..?
                await DataSeeder.SeedAsync(tenant.Id);
            }
            return ObjectMapper.Map<Tenant, TenantDto>(tenant);
        }

        [Authorize(AbpSaasManagementPermissions.Tenants.Update)]
        public override async Task<TenantDto> UpdateAsync(Guid id, TenantUpdateDto input)
        {
            var tenant = await SaasTenantRepository.GetAsync(id);
            await SaasTenantManager.ChangeNameAsync(tenant, input.Name);
            await SaasTenantManager.SetEdition(tenant, input.EditionId);
            await SaasTenantRepository.UpdateAsync(tenant);
            return ObjectMapper.Map<Tenant, TenantDto>(tenant);
        }

        [Authorize(AbpSaasManagementPermissions.Tenants.Default)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [Authorize(AbpSaasManagementPermissions.Tenants.ManageConnectionStrings)]
        public virtual async Task<string> GetDefaultConnectionStringAsync(Guid id)
        {
            var tenant = await SaasTenantRepository.GetAsync(id);
            return tenant?.FindDefaultConnectionString();
        }

        [Authorize(AbpSaasManagementPermissions.Tenants.ManageConnectionStrings)]
        public virtual async Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString)
        {
            var tenant = await SaasTenantRepository.GetAsync(id);
            tenant.SetDefaultConnectionString(defaultConnectionString);
            await SaasTenantRepository.UpdateAsync(tenant);
        }

        [Authorize(AbpSaasManagementPermissions.Tenants.ManageConnectionStrings)]
        public virtual async Task DeleteDefaultConnectionStringAsync(Guid id)
        {
            var tenant = await SaasTenantRepository.GetAsync(id);
            tenant.RemoveDefaultConnectionString();
            await SaasTenantRepository.UpdateAsync(tenant);
        }


    }
}
