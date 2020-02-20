using Taitans.Abp.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Taitans.Abp.Identity.Authorization
{
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.GetGroupOrNull(Volo.Abp.Identity.IdentityPermissions.GroupName);

            var claimTypesPermission = identityGroup.AddPermission(IdentityPermissions.ClaimTypes.Default, L("Permission:ClaimManagement"));
            claimTypesPermission.AddChild(IdentityPermissions.ClaimTypes.Create, L("Permission:Create"));
            claimTypesPermission.AddChild(IdentityPermissions.ClaimTypes.Update, L("Permission:Edit"));
            claimTypesPermission.AddChild(IdentityPermissions.ClaimTypes.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}