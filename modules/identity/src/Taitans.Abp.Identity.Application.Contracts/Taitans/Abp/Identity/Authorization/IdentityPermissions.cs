using Volo.Abp.Reflection;

namespace Taitans.Abp.Identity.Authorization
{
    public static class IdentityPermissions
    {
        public const string GroupName = "AbpIdentity";


        public static class ClaimTypes
        {
            public const string Default = GroupName + ".ClaimTypes";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityPermissions));
        }
    }
}