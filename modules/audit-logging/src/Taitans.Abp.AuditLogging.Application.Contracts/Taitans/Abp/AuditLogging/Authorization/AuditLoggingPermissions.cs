using Volo.Abp.Reflection;

namespace Taitans.Abp.AuditLogging.Authorization
{
    public class AuditLoggingPermissions
    {
        public const string GroupName = "AuditLogging";

        public static class AuditLogs
        {
            public const string Default = GroupName + ".AuditLogs"; 
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuditLoggingPermissions));
        }
    }
}