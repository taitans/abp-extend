using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.OcelotManagement
{
    public interface IOcelotAppService : ICrudAppService<
             OcelotDto,
             Guid,
             PagedAndSortedResultRequestDto,
             OcelotCreateDto,
             OcelotUpdateDto>, IApplicationService
    {
        Task<IList<OcelotReRouteDto>> GetReRoutesAsync(Guid id);
        Task<IList<OcelotReRouteDto>> UpdateReRoutesAsync(Guid id, List<OcelotReRouteDto> input);
        Task UpdateGatewayRoutesAsync(Guid id);
    }
}
