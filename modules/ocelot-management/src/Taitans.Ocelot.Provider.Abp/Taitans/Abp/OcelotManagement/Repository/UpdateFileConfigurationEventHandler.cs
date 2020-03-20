using Ocelot.Configuration.Repository;
using Taitans.Abp.OcelotManagement;
using Taitans.Ocelot.Provider.Abp.Configuration;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Taitans.Ocelot.Provider.Abp.Repository
{
    public class UpdateFileConfigurationEventHandler : IDistributedEventHandler<UpdateFileConfigurationEventEventData>, ITransientDependency
    {
        private readonly ConfigCacheOptions _option;
        private readonly IFileConfigurationRepository _fileConfigurationRepository;
        private readonly IAbpFileConfigurationRepository _abpFileConfigurationRepository;

        public string Name { get; protected set; }

        public UpdateFileConfigurationEventHandler(ConfigCacheOptions option,
            IFileConfigurationRepository fileConfigurationRepository,
            IAbpFileConfigurationRepository abpFileConfigurationRepository)
        {
            _option = option;
            _fileConfigurationRepository = fileConfigurationRepository;
            _abpFileConfigurationRepository = abpFileConfigurationRepository;
        }
        public async Task HandleEventAsync(UpdateFileConfigurationEventEventData eventData)
        {
            Name = eventData.Name;
            if (eventData.Name == _option.GatewayName)
            {
                var config = await _abpFileConfigurationRepository.GetFileConfiguration(eventData.Name);
                await _fileConfigurationRepository.Set(config);
            }
        }
    }
}
