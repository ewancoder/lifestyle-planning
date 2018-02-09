namespace Lifestyle.Shared.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    internal static class TestHelper
    {
        public static void ThrowsNull(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
                Assert.Throws<ArgumentNullException>(action);
        }
    }
}
