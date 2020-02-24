using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using System;
using Taitans.Ocelot.Provider.Abp.Configuration;
using Taitans.Ocelot.Provider.Abp.Repository;

namespace Taitans.Ocelot.Provider.Abp.DependencyInjection
{
    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddAbpOcelot(this IOcelotBuilder builder, Action<ConfigCacheOptions> option)
        {
            builder.Services.Configure(option);
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<ConfigCacheOptions>>().Value);

            #region 注入其他配置信息
            //重写提取Ocelot配置信息
            builder.Services.AddSingleton(DataBaseConfigurationProvider.Get);
            builder.Services.AddSingleton<IFileConfigurationRepository, AbpEfCoreFileConfigurationRepository>();

            #endregion
            return builder;
        }
    }
}
