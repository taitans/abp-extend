namespace Taitans.Abp.OcelotManagement
{
    public class QoSOptionDtoBase
    {
        public int ExceptionsAllowedBeforeBreaking { get; set; }
        public int DurationOfBreak { get; set; }
        public int TimeoutValue { get; set; }
    }
}
