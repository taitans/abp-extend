using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;

namespace Taitans.Abp.Identity
{
    public class UpdateIdentityProfileDto
    {
        [StringLength(IdentityUserConsts.MaxUserNameLength)]
        public string UserName { get; set; }

        [StringLength(IdentityUserConsts.MaxEmailLength)]
        public string Email { get; set; }

        [StringLength(IdentityUserConsts.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(IdentityUserConsts.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(IdentityUserConsts.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
    }
}
