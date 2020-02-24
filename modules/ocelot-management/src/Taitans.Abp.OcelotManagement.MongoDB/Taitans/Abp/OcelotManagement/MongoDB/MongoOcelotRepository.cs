using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Taitans.Abp.OcelotManagement.MongoDB
{
    public class MongoOcelotRepository : MongoDbRepository<IAbpOcelotManagementMongoDbContext, Ocelot, Guid>, IOcelotRepository
    {
        public MongoOcelotRepository(IMongoDbContextProvider<IAbpOcelotManagementMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Ocelot> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable()
                .FirstOrDefaultAsync(t => t.Name == name, GetCancellationToken(cancellationToken));
        }

        public async Task<IList<OcelotReRoute>> GetReRoutesAsync(Guid id)
        {
            return (await GetMongoQueryable()
                .FirstOrDefaultAsync(t => t.Id == id))?.ReRoutes;

            //return await DbContext.Set<OcelotReRoute>()
            //  .Include(c => c.DownstreamHostAndPorts)
            //  .Include(c => c.UpstreamHttpMethods)
            //  .Include(c => c.LoadBalancerOptions)
            //  .Where(c => c.GlobalConfigurationId == id).OrderBy(c => c.Sort).ToListAsync();
        }
    }
}
