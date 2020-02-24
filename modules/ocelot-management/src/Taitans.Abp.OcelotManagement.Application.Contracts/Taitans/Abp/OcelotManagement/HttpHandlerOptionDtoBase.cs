namespace Taitans.Abp.OcelotManagement
{
    public abstract class HttpHandlerOptionDtoBase
    {
        public bool? AllowAutoRedirect { get; set; }
        public bool? UseCookieContainer { get; set; }
        public bool? UseTracing { get; set; }
        public bool? UseProxy { get; set; }
    }
}
