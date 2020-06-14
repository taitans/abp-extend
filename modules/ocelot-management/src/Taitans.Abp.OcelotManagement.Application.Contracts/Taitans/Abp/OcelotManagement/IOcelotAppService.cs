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
        Task<List<OcelotRouteDto>> GetRoutesAsync(Guid id);
        Task<List<OcelotRouteDto>> UpdateRoutesAsync(Guid id, List<OcelotRouteDto> input);
        Task Reload(Guid id);
    }
}
