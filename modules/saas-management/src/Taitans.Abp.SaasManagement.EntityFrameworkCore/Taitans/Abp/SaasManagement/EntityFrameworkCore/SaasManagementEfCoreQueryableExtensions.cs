using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    public static class SaasManagementEfCoreQueryableExtensions
    {
        public static IQueryable<Tenant> IncludeDetails(this IQueryable<Tenant> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(c => c.ConnectionStrings)
                .Include(c => c.Edition);
        }

        public static IQueryable<Edition> IncludeDetails(this IQueryable<Edition> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable;
        }
    }
}
