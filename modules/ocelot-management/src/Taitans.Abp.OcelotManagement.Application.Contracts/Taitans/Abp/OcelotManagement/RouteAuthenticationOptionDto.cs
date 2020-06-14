using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteAuthenticationOptionDto
    {
        public string AuthenticationProviderKey { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}