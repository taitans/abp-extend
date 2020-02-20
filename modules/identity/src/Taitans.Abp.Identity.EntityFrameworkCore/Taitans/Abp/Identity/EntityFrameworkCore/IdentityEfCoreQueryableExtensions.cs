using Microsoft.EntityFrameworkCore;
using System.Linq;
using Volo.Abp.Identity;

namespace Taitans.Abp.Identity.EntityFrameworkCore
{
    public static class IdentityEfCoreQueryableExtensions
    {
        public static IQueryable<IdentityClaimType> IncludeDetails(this IQueryable<IdentityClaimType> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.ValueType);
        }
    }
}
