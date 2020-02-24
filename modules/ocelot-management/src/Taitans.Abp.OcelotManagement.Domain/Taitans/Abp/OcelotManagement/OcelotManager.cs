using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotManager : DomainService, IOcelotManager
    {
        private readonly IOcelotRepository _ocelotRepository;

        public OcelotManager(IOcelotRepository ocelotRepository)
        {
            _ocelotRepository = ocelotRepository;
        }

        public async Task<Ocelot> CreateAsync(string name)
        {
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name);
            return new Ocelot(GuidGenerator.Create(), name);
        }

        protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var ocelot = await _ocelotRepository.FindByNameAsync(name);
            if (ocelot != null && ocelot.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate ocelot name: " + name); //TODO: A domain exception would be better..?
            }
        }
    }
}
