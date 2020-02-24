using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Taitans.Abp.AuditLogging.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;

namespace Taitans.Abp.AuditLogging
{
    [Authorize(AuditLoggingPermissions.AuditLogs.Default)]
    public class AuditLogAppService : CrudAppService<AuditLog,
        AuditLogDto,
        Guid,
        GetAuditLoggingInput>, IAuditLogAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogAppService(IAuditLogRepository auditLogRepository) : base(auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public override async Task<AuditLogDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AuditLog, AuditLogDto>(
                await _auditLogRepository.GetAsync(id));
        }

        public override async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLoggingInput input)
        {
            var count = await _auditLogRepository.GetCountAsync(
                null,
                null,
                input.HttpMethod,
                input.Url,
                input.UserName,
                input.ApplicationName,
                input.CorrelationId,
                input.MaxExecutionDuration,
                input.MinExecutionDuration,
                input.HasException,
                input.HttpStatusCode);

            List<AuditLog> list = null;
            if (count > 0)
            {
                list = await _auditLogRepository.GetListAsync(
                    input.Sorting,
                    input.MaxResultCount,
                    input.SkipCount,
                    null, null,
                    input.HttpMethod,
                    input.Url,
                    input.UserName,
                    input.ApplicationName,
                    input.CorrelationId,
                    input.MaxExecutionDuration,
                    input.MinExecutionDuration,
                    input.HasException,
                    input.HttpStatusCode);
            }



            return new PagedResultDto<AuditLogDto>(
                count,
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list));
        }
    }
}
