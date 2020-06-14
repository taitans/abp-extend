namespace Taitans.Abp.OcelotManagement
{
    public class RouteHttpHandlerOptionDto : HttpHandlerOptionDtoBase
    {
        public int? MaxConnectionsPerServer { get; set; }
    }
}
