namespace Taitans.Abp.AuditLogging
{
    public static class AuditLoggingDbProperties
    {
        public static string DbTablePrefix { get; set; } = "AuditLogging";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "AuditLogging";
    }
}
