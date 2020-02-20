using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Taitans.Abp.Identity.EntityFrameworkCore
{
    public class EfCoreIdentityClaimTypeRepository : Volo.Abp.Identity.EntityFrameworkCore.EfCoreIdentityClaimTypeRepository, IIdentityClaimTypeRepository
    {
        public EfCoreIdentityClaimTypeRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<string>> GetClaimTypesAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.Select(c => c.Name).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            return await this.WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                    ).LongCountAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
        }

        public override IQueryable<IdentityClaimType> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}
