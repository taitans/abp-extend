using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taitans.Abp.OcelotManagement.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.OcelotManagement
{

    public class OcelotAppService : CrudAppService<
             Ocelot,
             OcelotDto,
             Guid,
             PagedAndSortedResultRequestDto,
             OcelotCreateDto,
             OcelotUpdateDto>, IOcelotAppService
    {
        protected IOcelotRepository OcelotRepository { get; }
        protected IOcelotManager OcelotManager { get; }

        public OcelotAppService(IOcelotRepository ocelotRepository,
            IOcelotManager ocelotManager) : base(ocelotRepository)
        {
            OcelotRepository = ocelotRepository;
            OcelotManager = ocelotManager;
        }

        public override async Task<OcelotDto> GetAsync(Guid id)
        {
            var entity = await OcelotRepository.GetAsync(id).ConfigureAwait(false);
            return ObjectMapper.Map<Ocelot, OcelotDto>(entity);
        }

        public virtual async Task<OcelotWithDetailsDto> FindByNameAsync(string name)
        {
            var config = await OcelotRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Ocelot, OcelotWithDetailsDto>(config);
        }

        public virtual async Task<IList<OcelotReRouteDto>> GetReRoutesAsync(Guid id)
        {
            var reRoutes = await OcelotRepository.GetReRoutesAsync(id);

            return ObjectMapper.Map<IList<OcelotReRoute>, IList<OcelotReRouteDto>>(reRoutes);
        }

        public virtual async Task<IList<OcelotReRouteDto>> UpdateReRoutesAsync(Guid id, List<OcelotReRouteDto> input)
        {
            var entity = await OcelotRepository.GetAsync(id).ConfigureAwait(false);

            ObjectMapper.Map(input, entity.ReRoutes);

            await OcelotRepository.UpdateAsync(entity, true).ConfigureAwait(false);

            return ObjectMapper.Map<IList<OcelotReRoute>, IList<OcelotReRouteDto>>(entity.ReRoutes);
        }

        public override async Task<OcelotDto> UpdateAsync(Guid id, OcelotUpdateDto input)
        {
            var entity = await GetEntityByIdAsync(id).ConfigureAwait(false);

            MapToEntity(input, entity);
            await OcelotRepository.UpdateAsync(entity).ConfigureAwait(false);

            return MapToGetOutputDto(entity);
        }

        public override async Task<PagedResultDto<OcelotDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await base.GetListAsync(input);
        }

        [Authorize(AbpOcelotManagementPermissions.Ocelots.Create)]
        public override async Task<OcelotDto> CreateAsync(OcelotCreateDto input)
        {
            var ocelot = await OcelotManager.CreateAsync(input.Name);
            await OcelotRepository.InsertAsync(ocelot);

            return ObjectMapper.Map<Ocelot, OcelotDto>(ocelot);
        }

        [Authorize(AbpOcelotManagementPermissions.Ocelots.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }
    }
}
