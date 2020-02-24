using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotWithDetailsDto : OcelotDtoBase
    {
        public IList<OcelotReRouteDto> OcelotReRoutes { get; set; }
    }
}
