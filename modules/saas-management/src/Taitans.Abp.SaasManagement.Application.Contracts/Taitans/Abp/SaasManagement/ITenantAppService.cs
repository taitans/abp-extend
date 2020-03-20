using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.SaasManagement
{
    public interface ITenantAppService : ICrudAppService<
        TenantDto,
        Guid,
        GetTenantInput, TenantCreateDto, TenantUpdateDto>
    {
        Task<string> GetDefaultConnectionStringAsync(Guid id);

        Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString);

        Task DeleteDefaultConnectionStringAsync(Guid id);
    }
}
