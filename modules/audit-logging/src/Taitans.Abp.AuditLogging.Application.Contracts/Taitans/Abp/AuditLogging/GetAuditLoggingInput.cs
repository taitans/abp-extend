using System;
using System.Net;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.AuditLogging
{
    public class GetAuditLoggingInput : PagedAndSortedResultRequestDto
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string CorrelationId { get; set; }
        public string HttpMethod { get; set; }
        public HttpStatusCode? HttpStatusCode { get; set; }
        public int? MaxExecutionDuration { get; set; }
        public int? MinExecutionDuration { get; set; }
        public bool? HasException { get; set; }
    }
}
