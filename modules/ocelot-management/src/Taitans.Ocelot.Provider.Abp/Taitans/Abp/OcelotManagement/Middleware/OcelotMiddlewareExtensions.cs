using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Configuration;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responses;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Taitans.Ocelot.Provider.Abp.Middleware
{
    public static class OcelotMiddlewareExtensions
    {
        /// <summary>
        /// Abp EntityFramwork形式读取数据库
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseAbpOcelot(this IApplicationBuilder builder)
        {
            await builder.UseOcelot(new OcelotPipelineConfiguration());

            return builder;
        }


        private static async Task<IInternalConfiguration> CreateConfiguration(IApplicationBuilder builder)
        {
            var fileConfig = await builder.ApplicationServices.GetService<IFileConfigurationRepository>().Get();

            // now create the config
            var internalConfigCreator = builder.ApplicationServices.GetService<IInternalConfigurationCreator>();
            var internalConfig = await internalConfigCreator.Create(fileConfig.Data);

            //Configuration error, throw error message
            if (internalConfig.IsError)
            {
                ThrowToStopOcelotStarting(internalConfig);
            }

            // now save it in memory
            var internalConfigRepo = builder.ApplicationServices.GetService<IInternalConfigurationRepository>();
            internalConfigRepo.AddOrReplace(internalConfig.Data);


            var configurations = builder.ApplicationServices.GetServices<OcelotMiddlewareConfigurationDelegate>();

            foreach (var configuration in configurations)
            {
                await configuration(builder);
            }


            return GetOcelotConfigAndReturn(internalConfigRepo);
        }

        private static void ThrowToStopOcelotStarting(Response config)
        {
            throw new Exception($"Unable to start Ocelot, errors are: {string.Join(",", config.Errors.Select(x => x.ToString()))}");
        }

        private static IInternalConfiguration GetOcelotConfigAndReturn(IInternalConfigurationRepository provider)
        {
            var ocelotConfiguration = provider.Get();

            if (ocelotConfiguration?.Data == null || ocelotConfiguration.IsError)
            {
                ThrowToStopOcelotStarting(ocelotConfiguration);
            }

            return ocelotConfiguration.Data;
        }

        private static void ConfigureDiagnosticListener(IApplicationBuilder builder)
        {
            var env = builder.ApplicationServices.GetService<IWebHostEnvironment>();
            var listener = builder.ApplicationServices.GetService<OcelotDiagnosticListener>();
            var diagnosticListener = builder.ApplicationServices.GetService<DiagnosticListener>();
            diagnosticListener.SubscribeWithAdapter(listener);
        }
    }
}
