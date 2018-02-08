namespace Lifestyle.Shared.Tests.Identity
{
    using Xunit;

    [Trait("Category", "Identity")]
    public class ReferenceTypeIdentityTests : IdentityTests<ReferenceTestIdentity, string>
    {
        protected override string SameValue() => "same value";
        protected override string AnotherValue() => "another value";
        protected override ReferenceTestIdentity CreateIdentity(string value)
            => new ReferenceTestIdentity(value);
    }
}
