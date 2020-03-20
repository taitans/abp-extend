using Ocelot.Configuration.File;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Taitans.Ocelot.Provider.Abp.Repository
{
    public interface IAbpFileConfigurationRepository : ITransientDependency
    {
        Task<FileConfiguration> GetFileConfiguration(string name);
    }
}
