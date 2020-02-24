﻿using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotReRouteDto
    {
        public string Name { get; set; }

        public int Timeout { get; set; }
        public int Priority { get; set; }
        public List<ReRouteDownstreamHostAndPortDto> DownstreamHostAndPorts { get; set; }
        public ReRouteLoadBalancerOptionDto LoadBalancerOption { get; set; }
        public Dictionary<string, string> AddQueriesToRequests { get; set; }
        public Dictionary<string, string> ChangeDownstreamPathTemplates { get; set; }
        public List<string> UpstreamHttpMethods { get; set; }
        public int? Sort { get; set; }
        public string DownstreamHttpMethod { get; set; }
        public string ServiceNamespace { get; set; }
    }
}