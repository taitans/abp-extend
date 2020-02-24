using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Taitans.Abp.OcelotManagement
{
    public class Ocelot_Tests : AbpOcelotManagementDomainTestBase
    {
        private readonly IOcelotRepository _ocelotRepository;

        public Ocelot_Tests()
        {
            _ocelotRepository = GetRequiredService<IOcelotRepository>();
        }

        [Fact]
        public async Task GetReRoutesAsync()
        {
            var ocelot = (await _ocelotRepository.GetListAsync()).First();
            var reRoute = await _ocelotRepository.GetReRoutesAsync(ocelot.Id);
            reRoute.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task FindByNameAsync()
        {
            var ocelot = await _ocelotRepository.FindByNameAsync("middleware").ConfigureAwait(false);
            ocelot.Name.ShouldBe("middleware");

        }


    }
}
