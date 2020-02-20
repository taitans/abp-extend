using Volo.Abp.Identity;

namespace Taitans.Abp.Identity
{
    public class CreateClaimTypeDto
    {
        public string Name { get; set; }
        public bool? Required { get; set; }
        public string Regex { get; set; }
        public string RegexDescription { get; set; }
        public string Description { get; set; }
        public IdentityClaimValueType ValueType { get; set; }
    }
}
