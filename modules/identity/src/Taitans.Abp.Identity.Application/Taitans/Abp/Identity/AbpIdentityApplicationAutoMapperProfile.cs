using AutoMapper;
using Volo.Abp.Identity;

namespace Taitans.Abp.Identity
{
    public class IdentityApplicationAutoMapperProfile : Profile
    {
        public IdentityApplicationAutoMapperProfile()
        {
            CreateMap<IdentityClaimType, ClaimTypeDto>();
            CreateMap<CreateClaimTypeDto, IdentityClaimType>();
            CreateMap<UpdateClaimTypeDto, IdentityClaimType>();
        }
    }
}