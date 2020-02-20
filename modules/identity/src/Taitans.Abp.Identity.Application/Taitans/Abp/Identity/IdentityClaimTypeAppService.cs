using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taitans.Abp.Identity;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using IdentityPermissions = Taitans.Abp.Identity.Authorization.IdentityPermissions;

namespace Taitans.MiddleGround.Identity
{
    public class IdentityClaimTypeAppService : CrudAppService<IdentityClaimType,
        ClaimTypeDto,
        Guid,
        GetIdentityClaimTypeInput,
        CreateClaimTypeDto,
        UpdateClaimTypeDto>, IIdentityClaimTypeAppService
    {
        private readonly IRepository<IdentityClaimType, Guid> _repository;
        private readonly Abp.Identity.IIdentityClaimTypeRepository _identityClaimTypeRepository;

        public IdentityClaimTypeAppService(IRepository<IdentityClaimType, Guid> repository,
            Abp.Identity.IIdentityClaimTypeRepository identityClaimTypeRepository) : base(repository)
        {
            _repository = repository;
            _identityClaimTypeRepository = identityClaimTypeRepository;
        }


        [Authorize(IdentityPermissions.ClaimTypes.Default)]
        public async Task<List<ClaimTypeDto>> GetAll()
        {
            var typeList = await _repository.GetListAsync();

            return ObjectMapper.Map<List<IdentityClaimType>, List<ClaimTypeDto>>(typeList);
        }

        [Authorize(IdentityPermissions.ClaimTypes.Default)]
        public async override Task<PagedResultDto<ClaimTypeDto>> GetListAsync(GetIdentityClaimTypeInput input)
        {
            var count = await _identityClaimTypeRepository.GetCountAsync(input.Filter);
            var list = await _identityClaimTypeRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<ClaimTypeDto>(
                count,
                ObjectMapper.Map<List<IdentityClaimType>, List<ClaimTypeDto>>(list));
        }

        [Authorize(IdentityPermissions.ClaimTypes.Create)]
        public override Task<ClaimTypeDto> CreateAsync(CreateClaimTypeDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(IdentityPermissions.ClaimTypes.Delete)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        [Authorize(IdentityPermissions.ClaimTypes.Update)]
        public override Task<ClaimTypeDto> UpdateAsync(Guid id, UpdateClaimTypeDto input)
        {
            return base.UpdateAsync(id, input);
        }

        public async Task<List<string>> GetClaimTypes()
        {
            return await _identityClaimTypeRepository.GetClaimTypesAsync();
        }
    }
}
