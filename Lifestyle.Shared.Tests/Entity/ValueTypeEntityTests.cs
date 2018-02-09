namespace Lifestyle.Shared.Tests.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "Entity")]
    public class ValueTypeEntityTests : EntityTests<ValueTestEntity, int>
    {
        protected override int SameIdentity() => 10;
        protected override int AnotherIdentity() => 20;
        protected override ValueTestEntity CreateEntity(int identity)
            => new ValueTestEntity(identity);

        protected override IEnumerable<Action> ShouldThrowNullActions()
            => Enumerable.Empty<Action>();
    }
}
