using AutoMapper;
using Volo.Abp.AuditLogging;

namespace Taitans.Abp.AuditLogging
{
    public class AbpAuditLoggingApplicationAutoMapperProfile : Profile
    {
        public AbpAuditLoggingApplicationAutoMapperProfile()
        {
            CreateMap<AuditLog, AuditLogDto>();
            CreateMap<AuditLogAction, AuditLogActionDto>();
        }
    }
}