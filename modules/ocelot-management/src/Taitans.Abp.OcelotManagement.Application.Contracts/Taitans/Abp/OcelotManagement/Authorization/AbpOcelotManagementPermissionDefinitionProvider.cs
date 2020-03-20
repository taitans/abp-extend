using Taitans.Abp.OcelotManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Taitans.Abp.OcelotManagement.Authorization
{
    public class AbpOcelotPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var ocelotGroup = context.AddGroup(AbpOcelotManagementPermissions.GroupName, L("Permission:OcelotManagement"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);

            var ocelotssPermission = ocelotGroup.AddPermission(AbpOcelotManagementPermissions.Ocelots.Default, L("Permission:OcelotManagement"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
            ocelotssPermission.AddChild(AbpOcelotManagementPermissions.Ocelots.Create, L("Permission:Create"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
            ocelotssPermission.AddChild(AbpOcelotManagementPermissions.Ocelots.Update, L("Permission:Edit"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
            ocelotssPermission.AddChild(AbpOcelotManagementPermissions.Ocelots.Delete, L("Permission:Delete"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpOcelotManagementResource>(name);
        }
    }
}