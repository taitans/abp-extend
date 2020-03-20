using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Taitans.Ocelot.Provider.Abp.Configuration;
using Taitans.Ocelot.Provider.Abp.Repository;
using System;

namespace Taitans.Ocelot.Provider.Abp.DependencyInjection
{
    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddAbpOcelot(this IOcelotBuilder builder, Action<ConfigCacheOptions> option)
        {
            builder.Services.Configure(option);
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<ConfigCacheOptions>>().Value);


            builder.Services.AddSingleton(DataBaseConfigurationProvider.Get);
            builder.Services.AddSingleton<IFileConfigurationRepository, FileConfigurationRepository>();

            return builder;
        }
    }
}
