namespace Lifestyle.Shared.Tests
{
    using System;
    using Xunit;

    [Trait("Category", "Guard")]
    public class GuardTests
    {
        [Fact(DisplayName = "Should throw if object is null")]
        public void ShouldThrowIfObjectIsNull()
        {
            var paramName = "paramName";

            var exception = Assert.Throws<ArgumentNullException>(
                () => Guard.ThrowIfNull<object>(null, paramName));

            Assert.Equal(paramName, exception.ParamName);
        }

        [Fact(DisplayName = "Should not throw if object is not null")]
        public void ShouldNotThrowIfObjectIsNotNull()
        {
            Guard.ThrowIfNull(string.Empty, string.Empty);
        }
    }
}
