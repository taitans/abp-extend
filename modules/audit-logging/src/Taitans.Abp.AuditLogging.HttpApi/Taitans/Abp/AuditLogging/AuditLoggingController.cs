using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks; 
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.AuditLogging
{
    [RemoteService]
    [Area("AuditLogs")]
    [ControllerName("AuditLogs")]
    [Route("/api/audit-logging/audit-logs")]
    [Authorize]
    public class AuditLoggingController : AuditLoggingControllerBase
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLoggingController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [HttpGet("{id}")]
        public async Task<AuditLogDto> GetAsync(Guid id)
        {
            return await _auditLogAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLoggingInput input)
        {
            return await _auditLogAppService.GetListAsync(input);
        }
    }
}
