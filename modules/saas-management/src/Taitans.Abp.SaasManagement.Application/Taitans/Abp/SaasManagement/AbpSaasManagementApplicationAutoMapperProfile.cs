using AutoMapper;
namespace Taitans.Abp.SaasManagement
{
    public class AbpSaasManagementApplicationAutoMapperProfile : Profile
    {
        public AbpSaasManagementApplicationAutoMapperProfile()
        {
            CreateMap<Tenant, TenantDto>();

            CreateMap<Edition, EditionDto>();

        }
    }
}