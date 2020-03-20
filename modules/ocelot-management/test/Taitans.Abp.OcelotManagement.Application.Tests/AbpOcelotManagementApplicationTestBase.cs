using Taitans.Abp.OcelotManagement.EntityFrameworkCore;
using System;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class AbpOcelotManagementApplicationTestBase : OcelotManagementTestBase<AbpOcelotManagementApplicationTestModule>
    {
        protected virtual void UsingDbContext(Action<IOcelotManagementDbContext> action)
        {
            using (var dbContext = GetRequiredService<IOcelotManagementDbContext>())
            {
                action.Invoke(dbContext);
            }
        }

        protected virtual T UsingDbContext<T>(Func<IOcelotManagementDbContext, T> action)
        {
            using (var dbContext = GetRequiredService<IOcelotManagementDbContext>())
            {
                return action.Invoke(dbContext);
            }
        }
    }
}