using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Taitans.Abp.AuditLogging
{
    public interface IAuditLogAppService : ICrudAppService<
        AuditLogDto,
        Guid,
        GetAuditLoggingInput>
    {
    }
}
