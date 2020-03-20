using System;
using System.Collections.Generic;

namespace Taitans.Abp.SaasManagement
{
    public interface ITenant
    {
        string Name { get; }

        string DisplayName { get; }

        List<TenantConnectionString> ConnectionStrings { get; }

        Edition Edition { get; }

        Guid? EditionId { get; }

        string EditionName { get; }
    }
}