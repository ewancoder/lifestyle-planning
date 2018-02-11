namespace Lifestyle.Planning.Domain.Tests
{
    using Xunit;
    using Shared.Tests;

    [Trait("Category", "Stage identity")]
    public class StageIdentityTests : IdentityTests<StageId, int>
    {
        protected override int SameValue() => 1;
        protected override int AnotherValue() => 10;
        protected override StageId CreateIdentity(int value)
            => new StageId(value);
    }
}
