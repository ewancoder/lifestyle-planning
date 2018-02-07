namespace Lifestyle.Shared.Tests.Entity
{
    using Xunit;

    [Trait("Category", "Entity")]
    public class ReferenceTypeEntityTests : EntityTests<ReferenceTestEntity, string>
    {
        protected override string SameIdentity() => "same value";
        protected override string AnotherIdentity() => "another value";
        protected override ReferenceTestEntity CreateEntity(string identity)
            => new ReferenceTestEntity(identity);
    }
}
