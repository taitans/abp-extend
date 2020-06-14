using Newtonsoft.Json;
using Ocelot.Cache;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;
using Ocelot.Responses;
using Taitans.Ocelot.Provider.Abp.Configuration;
using System;
using System.Threading.Tasks;

namespace Taitans.Ocelot.Provider.Abp.Repository
{
    public class FileConfigurationRepository : IFileConfigurationRepository
    {
        private readonly ConfigCacheOptions _option;
        private readonly IOcelotCache<FileConfiguration> _cache;
        private readonly IAbpFileConfigurationRepository _abpFileConfigurationRepository;
        private readonly IOcelotLogger _logger;

        public FileConfigurationRepository(
            ConfigCacheOptions option,
            IOcelotCache<FileConfiguration> cache,
            IAbpFileConfigurationRepository abpFileConfigurationRepository,
            IOcelotLoggerFactory loggerFactory)
        {
            _option = option;
            _cache = cache;
            _abpFileConfigurationRepository = abpFileConfigurationRepository;
            _logger = loggerFactory.CreateLogger<FileConfigurationRepository>();
        }
        public async Task<Response<FileConfiguration>> Get()
        {
            var config = _cache.Get(_option.CachePrefix + "FileConfiguration", "");

            if (config != null)
            {
                return new OkResponse<FileConfiguration>(config);
            }
            var file = await _abpFileConfigurationRepository.GetFileConfiguration(_option.GatewayName);
            if (file.Routes == null || file.Routes.Count == 0)
            {
                return new OkResponse<FileConfiguration>(null);
            }
            _logger.LogDebug(JsonConvert.SerializeObject(file));

            return new OkResponse<FileConfiguration>(file);
        }



        public Task<Response> Set(FileConfiguration fileConfiguration)
        {
            _cache.AddAndDelete(_option.CachePrefix + "FileConfiguration", fileConfiguration, TimeSpan.FromSeconds(1800), "");
            return Task.FromResult((Response)new OkResponse());
        }
    }
}
