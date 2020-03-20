using Taitans.Abp.SaasManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Taitans.Abp.SaasManagement.Authorization
{
    public class AbpSaasManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var SaasManagement = context.AddGroup(AbpSaasManagementPermissions.GroupName, L("Permission:SaasManagement"));

            var tenantPermission = SaasManagement.AddPermission(AbpSaasManagementPermissions.Tenants.Default, L("Permission:SaasManagement"));
            tenantPermission.AddChild(AbpSaasManagementPermissions.Tenants.Create, L("Permission:Create"));
            tenantPermission.AddChild(AbpSaasManagementPermissions.Tenants.Update, L("Permission:Edit"));
            tenantPermission.AddChild(AbpSaasManagementPermissions.Tenants.Delete, L("Permission:Delete"));
            tenantPermission.AddChild(AbpSaasManagementPermissions.Tenants.ManageFeatures, L("Permission:ManageFeatures"));
            tenantPermission.AddChild(AbpSaasManagementPermissions.Tenants.ManageConnectionStrings, L("Permission:ManageConnectionStrings"));

            var editionPermission = SaasManagement.AddPermission(AbpSaasManagementPermissions.Editions.Default, L("Permission:EditionManagement"));
            editionPermission.AddChild(AbpSaasManagementPermissions.Editions.Create, L("Permission:Create"));
            editionPermission.AddChild(AbpSaasManagementPermissions.Editions.Update, L("Permission:Edit"));
            editionPermission.AddChild(AbpSaasManagementPermissions.Editions.Delete, L("Permission:Delete"));
            editionPermission.AddChild(AbpSaasManagementPermissions.Editions.ManageFeatures, L("Permission:ManageFeatures"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSaasManagementResource>(name);
        }
    }
}