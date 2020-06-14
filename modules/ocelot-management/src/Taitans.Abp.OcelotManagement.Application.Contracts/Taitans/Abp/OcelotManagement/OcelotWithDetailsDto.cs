using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotWithDetailsDto : OcelotDtoBase
    {
        public List<OcelotRouteDto> OcelotRoutes { get; set; }
    }
}
