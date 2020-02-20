namespace Taitans.Abp.Identity.MongoDB
{
    /* This class can be used as a base class for MongoDB integration tests,
     * while SampleRepository_Tests uses a different approach.
     */
    public abstract class AbpIdentityMongoDbTestBase : AbpIdentityTestBase<AbpIdentityMongoDbTestModule>
    {

    }
}