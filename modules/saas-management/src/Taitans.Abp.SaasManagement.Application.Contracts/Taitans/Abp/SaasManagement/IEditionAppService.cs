using System;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.SaasManagement
{
    public interface IEditionAppService : ICrudAppService<
        EditionDto,
        Guid,
        GetEditionInput, EditionCreateDto, EditionUpdateDto>
    {
    }
}
