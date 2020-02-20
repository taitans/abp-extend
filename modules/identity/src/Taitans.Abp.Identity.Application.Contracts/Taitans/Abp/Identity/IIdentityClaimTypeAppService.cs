using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.Identity
{
    public interface IIdentityClaimTypeAppService : ICrudAppService<ClaimTypeDto,
        Guid,
        GetIdentityClaimTypeInput,
        CreateClaimTypeDto,
        UpdateClaimTypeDto>
    {
        Task<List<ClaimTypeDto>> GetAll();

        Task<List<string>> GetClaimTypes();
    }
}
