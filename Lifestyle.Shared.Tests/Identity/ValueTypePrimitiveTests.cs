namespace Lifestyle.Shared.Tests.Identity
{
    using Xunit;

    [Trait("Category", "Identity")]
    public class ValueTypeIdentityTests : IdentityTests<ValueTestIdentity, int>
    {
        protected override int SameValue() => 10;
        protected override int AnotherValue() => 20;
        protected override ValueTestIdentity CreateIdentity(int value)
            => new ValueTestIdentity(value);
    }
}
