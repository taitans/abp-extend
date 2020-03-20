namespace Taitans.Ocelot.Provider.Abp.Configuration
{
    public class ConfigCacheOptions
    {
        /// <summary>
        /// 缓存默认前缀，防止重复
        /// </summary>
        public string CachePrefix { get; set; } = "Taitans";
        /// <summary>
        /// 网关名
        /// </summary>
        public string GatewayName { get; set; }
    }
}