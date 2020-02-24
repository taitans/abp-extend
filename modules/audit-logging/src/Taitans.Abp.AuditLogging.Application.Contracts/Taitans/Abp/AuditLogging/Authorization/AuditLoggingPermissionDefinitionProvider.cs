using Taitans.Abp.AuditLogging.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Taitans.Abp.AuditLogging.Authorization
{
    public class AuditLoggingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var auditLoggingGroup = context.AddGroup(AuditLoggingPermissions.GroupName, L("Permission:AuditLogging"));

            auditLoggingGroup.AddPermission(AuditLoggingPermissions.AuditLogs.Default, L("Permission:AuditLogs"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuditLoggingResource>(name);
        }
    }
}