namespace Lifestyle.Shared.Tests.Entity
{
    using Xunit;

    [Trait("Category", "Entity")]
    public class ValueTypeEntityTests : EntityTests<ValueTestEntity, int>
    {
        protected override int SameIdentity() => 10;
        protected override int AnotherIdentity() => 20;
        protected override ValueTestEntity CreateEntity(int identity)
            => new ValueTestEntity(identity);
    }
}
