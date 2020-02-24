using Volo.Abp.Reflection;

namespace Taitans.Abp.OcelotManagement.Authorization
{
    public class AbpOcelotManagementPermissions
    {
        public const string GroupName = "AbpOcelotManagement";

        public static class Ocelots
        {
            public const string Default = GroupName + ".Ocelots";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AbpOcelotManagementPermissions));
        }
    }
}