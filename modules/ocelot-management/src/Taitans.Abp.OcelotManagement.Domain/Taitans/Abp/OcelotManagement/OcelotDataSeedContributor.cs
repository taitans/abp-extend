using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IOcelotRepository _ocelotRepository;
        private readonly IConfiguration _configuration;

        public OcelotDataSeedContributor(
            IGuidGenerator guidGenerator,
            IOcelotRepository ocelotRepository,
            IConfiguration configuration)
        {
            _guidGenerator = guidGenerator;
            _ocelotRepository = ocelotRepository;
            _configuration = configuration;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            var configurationSection = _configuration.GetSection("GlobalConfiguration");
            var baseUrl = configurationSection["BaseUrl"];
            var gatewayname = configurationSection["Name"];
            if (!string.IsNullOrWhiteSpace(gatewayname))
            {
                var ocelot = await _ocelotRepository.FindByNameAsync(gatewayname);
                if (ocelot == null)
                {
                    ocelot = new Ocelot(
                      _guidGenerator.Create(),
                      gatewayname,
                      null,
                      baseUrl);

                    int index = 0;
                    do
                    {
                        string name = configurationSection[$"ReRoutes:{index}:Name"];

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            break;
                        }
                        var UpstreamPathTemplate = configurationSection[$"ReRoutes:{index}:UpstreamPathTemplate"];
                        var DownstreamScheme = configurationSection[$"ReRoutes:{index}:DownstreamScheme"];
                        var DownstreamPathTemplate = configurationSection[$"ReRoutes:{index}:DownstreamPathTemplate"];

                        int methodIndex = 0;
                        List<string> methods = new List<string>();
                        do
                        {
                            string method = configurationSection[$"ReRoutes:{index}:UpstreamHttpMethod:{methodIndex++}"];
                            if (string.IsNullOrWhiteSpace(method))
                            {
                                break;
                            }
                            methods.Add(method);
                        } while (true);

                        int hostIndex = 0;
                        Dictionary<string, int> DownstreamHostAndPorts = new Dictionary<string, int>();
                        do
                        {
                            string host = configurationSection[$"ReRoutes:{index}:DownstreamHostAndPorts:{hostIndex}:Host"];
                            string port = configurationSection[$"ReRoutes:{index}:DownstreamHostAndPorts:{hostIndex}:Port"];
                            hostIndex++;
                            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(port))
                            {
                                break;
                            }
                            DownstreamHostAndPorts.Add(host, Convert.ToInt32(port));
                        } while (true);

                        ocelot.AddReRoutes(
                            name,
                            UpstreamPathTemplate,
                            null,
                            DownstreamScheme,
                            DownstreamPathTemplate,
                            methods,
                            DownstreamHostAndPorts
                            );

                        index++;

                    } while (true);

                    await _ocelotRepository.InsertAsync(ocelot);
                }
            }
        }
    }
}
