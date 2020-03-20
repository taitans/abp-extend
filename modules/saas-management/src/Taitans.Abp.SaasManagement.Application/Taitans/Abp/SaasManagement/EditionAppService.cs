using Microsoft.AspNetCore.Authorization;
using Taitans.Abp.SaasManagement.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.SaasManagement
{
    [Authorize(AbpSaasManagementPermissions.Editions.Default)]
    public class EditionAppService : CrudAppService<Edition,
        EditionDto,
        Guid,
        GetEditionInput, EditionCreateDto, EditionUpdateDto>, IEditionAppService
    {
        public IEditionRepository EditionRepository { get; }
        public IEditionManager EditionManager { get; }

        public EditionAppService(IEditionRepository repository,
            IEditionManager editionManager) : base(repository)
        {
            EditionRepository = repository;
            EditionManager = editionManager;
        }



        public override async Task<PagedResultDto<EditionDto>> GetListAsync(GetEditionInput input)
        {
            var count = await EditionRepository.GetCountAsync(input.Filter);
            var list = await EditionRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<EditionDto>(
                count,
                ObjectMapper.Map<List<Edition>, List<EditionDto>>(list));
        }


        public override Task<EditionDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        [Authorize(AbpSaasManagementPermissions.Editions.Create)]
        public override async Task<EditionDto> CreateAsync(EditionCreateDto input)
        {
            var edition = await EditionManager.CreateAsync(input.DisplayName);
            await EditionRepository.InsertAsync(edition);

            return ObjectMapper.Map<Edition, EditionDto>(edition);
        }

        [Authorize(AbpSaasManagementPermissions.Editions.Update)]
        public override async Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
        {
            var edition = await EditionRepository.GetAsync(id);
            await EditionManager.ChangeNameAsync(edition, input.DisplayName);
            await EditionRepository.UpdateAsync(edition);
            return ObjectMapper.Map<Edition, EditionDto>(edition);
        }

        [Authorize(AbpSaasManagementPermissions.Editions.Default)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }
    }
}
