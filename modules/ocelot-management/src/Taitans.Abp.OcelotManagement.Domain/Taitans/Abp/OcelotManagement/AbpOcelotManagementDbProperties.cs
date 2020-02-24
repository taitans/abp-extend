namespace Taitans.Abp.OcelotManagement
{
    public static class AbpOcelotManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Ocelot";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "AbpOcelotManagement";
    }
}
