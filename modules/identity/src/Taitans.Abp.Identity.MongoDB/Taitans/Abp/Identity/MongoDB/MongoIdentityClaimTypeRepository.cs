

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.Identity.MongoDB
{
    public class MongoIdentityClaimTypeRepository : Volo.Abp.Identity.MongoDB.MongoIdentityClaimTypeRepository, IIdentityClaimTypeRepository
    {
        public MongoIdentityClaimTypeRepository(IMongoDbContextProvider<IAbpIdentityMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<string>> GetClaimTypesAsync(CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable()
                 .Select(c => c.Name).ToListAsync(GetCancellationToken(GetCancellationToken(cancellationToken)));
        }

        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                return GetMongoQueryable()
                .WhereIf(
                       !filter.IsNullOrWhiteSpace(),
                       u =>
                           u.Name.Contains(filter)
                       ).ToList().Count;
            });
        }
    }
}
