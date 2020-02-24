using Taitans.Abp.OcelotManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Taitans.Abp.OcelotManagement.Authorization
{
    public class AbpOcelotPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var ocelotGroup = context.AddGroup(AbpOcelotManagementPermissions.GroupName, L("Permission:OcelotManagement"));

            var ocelotssPermission = ocelotGroup.AddPermission(AbpOcelotManagementPermissions.Ocelots.Default, L("Permission:OcelotManagement"));
            ocelotssPermission.AddChild(AbpOcelotManagementPermissions.Ocelots.Create, L("Permission:Create"));
            ocelotssPermission.AddChild(AbpOcelotManagementPermissions.Ocelots.Update, L("Permission:Edit"));
            ocelotssPermission.AddChild(AbpOcelotManagementPermissions.Ocelots.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpOcelotManagementResource>(name);
        }
    }
}