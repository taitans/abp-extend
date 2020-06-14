using Ocelot.Configuration.Repository;
using Taitans.Abp.OcelotManagement;
using Taitans.Ocelot.Provider.Abp.Configuration;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Microsoft.Extensions.Logging;
using Ocelot.Configuration.Creator;

namespace Taitans.Ocelot.Provider.Abp.Repository
{
    public class UpdateFileConfigurationEventHandler : IDistributedEventHandler<UpdateFileConfigurationEventEventData>, ITransientDependency
    {
        private readonly ConfigCacheOptions _option;
        private readonly IFileConfigurationRepository _fileConfigurationRepository;
        private readonly IAbpFileConfigurationRepository _abpFileConfigurationRepository;
        private readonly IInternalConfigurationRepository _internalConfigRepo;
        private readonly IInternalConfigurationCreator _internalConfigCreator;
        private readonly ILogger<UpdateFileConfigurationEventHandler> _logger;

        public string Name { get; protected set; }

        public UpdateFileConfigurationEventHandler(ConfigCacheOptions option,
            IFileConfigurationRepository fileConfigurationRepository,
            IAbpFileConfigurationRepository abpFileConfigurationRepository,
            IInternalConfigurationRepository internalConfigRepo,
            IInternalConfigurationCreator internalConfigCreator,
            ILogger<UpdateFileConfigurationEventHandler> logger)
        {
            _option = option;
            _fileConfigurationRepository = fileConfigurationRepository;
            _abpFileConfigurationRepository = abpFileConfigurationRepository;
            _internalConfigRepo = internalConfigRepo;
            _internalConfigCreator = internalConfigCreator;
            _logger = logger;
        }

        public async Task HandleEventAsync(UpdateFileConfigurationEventEventData eventData)
        {
            Name = eventData.Name;
            if (eventData.Name == _option.GatewayName)
            {
                _logger.LogInformation($"gateway name: {eventData.Name} reload sucess.");
                var fileConfiguration = await _abpFileConfigurationRepository.GetFileConfiguration(eventData.Name);

                var config = await _internalConfigCreator.Create(fileConfiguration);

                if (!config.IsError)
                {
                    _internalConfigRepo.AddOrReplace(config.Data);
                }
                 
                await _fileConfigurationRepository.Set(fileConfiguration);

            }
        }
    }
}
