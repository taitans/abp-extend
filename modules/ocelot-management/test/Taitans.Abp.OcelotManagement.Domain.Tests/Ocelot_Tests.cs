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
        public async Task GetRoutesAsync()
        {
            var ocelot = (await _ocelotRepository.GetListAsync()).First();
            var route = await _ocelotRepository.GetRoutesAsync(ocelot.Id);
            route.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task FindByNameAsync()
        {
            var ocelot = await _ocelotRepository.FindByNameAsync("middleware").ConfigureAwait(false);
            ocelot.Name.ShouldBe("middleware");

        }


    }
}
