namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using Xunit;
    using Shared.Tests;
    using System.Collections.Generic;

    [Trait("Category", "Stage")]
    public class StageTests : EntityTests<Stage, StageId>
    {
        protected override StageId SameIdentity() => new StageId(1);
        protected override StageId AnotherIdentity() => new StageId(10);
        protected override Stage CreateEntity(StageId identity)
            => new Stage(identity, Fixture.StageName());

        protected override IEnumerable<Action> ShouldThrowNullActions()
        {
            return new Action[]
            {
                () => new Stage(null, Fixture.StageName()),
                () => new Stage(Fixture.StageId(), null)
            };
        }
    }
}
