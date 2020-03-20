using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    public class EfCoreEditionRepository : EfCoreRepository<ISaasManagementDbContext, Edition, Guid>, IEditionRepository
    {
        public EfCoreEditionRepository(IDbContextProvider<ISaasManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Edition FindById(Guid id, bool includeDetails = true)
        {
            return DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Id == id);
        }

        public Edition FindByName(string name, bool includeDetails = true)
        {
            return DbSet.IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.DisplayName == name);
        }

        public async Task<Edition> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(t => t.DisplayName == name, GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await this
                     .WhereIf(
                         !filter.IsNullOrWhiteSpace(),
                         u =>
                             u.DisplayName.Contains(filter)
                     ).CountAsync(cancellationToken: cancellationToken);
        }

        public async Task<Edition> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync();
        }

        public async Task<List<Edition>> GetListAsync(string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.DisplayName.Contains(filter)
            )
            .OrderBy(sorting ?? nameof(Edition.DisplayName))
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override IQueryable<Edition> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}
