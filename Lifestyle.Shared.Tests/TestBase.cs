namespace Lifestyle.Shared.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public abstract class TestBase
    {
        protected abstract IEnumerable<Action> ShouldThrowNullActions();

        [Fact(DisplayName = "Shared: should throw null")]
        public void ShouldThrowNull()
        {
            foreach (var action in ShouldThrowNullActions())
                Assert.Throws<ArgumentNullException>(action);
        }
    }
}
