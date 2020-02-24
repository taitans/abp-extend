namespace Taitans.Abp.OcelotManagement
{
    public class LoadBalancerOptionDtoBase
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public int? Expiry { get; set; }
    }
}
