namespace Taitans.Abp.Identity
{
    public static class AbpIdentityDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Identity";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Identity";
    }
}
