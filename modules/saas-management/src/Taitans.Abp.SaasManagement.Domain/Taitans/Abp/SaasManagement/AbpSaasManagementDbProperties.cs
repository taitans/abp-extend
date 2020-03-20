namespace Taitans.Abp.SaasManagement
{
    public static class AbpSaasManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Saas";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "AbpSaasManagement";
    }
}
