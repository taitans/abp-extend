using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Xunit;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class LazyLoad_Tests<TStartupModule> : OcelotManagementTestBase<TStartupModule>
         where TStartupModule : IAbpModule
    {
        public IOcelotRepository OcelotRepository { get; }

        protected LazyLoad_Tests()
        {
            OcelotRepository = GetRequiredService<IOcelotRepository>();
        }

        [Fact]
        public async Task Should_Lazy_Load_Ocelot_Collections()
        {
            using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin())
            {
                var role = await OcelotRepository.FindByNameAsync("middleware", includeDetails: true);
                role.ReRoutes.ShouldNotBeNull();
                role.ReRoutes.Any().ShouldBeTrue();

                await uow.CompleteAsync();
            }
        }
    }
}
