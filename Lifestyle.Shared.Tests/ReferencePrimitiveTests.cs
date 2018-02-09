namespace Lifestyle.Shared.Tests
{
    using System;
    using Xunit;

    public abstract class ReferencePrimitiveTests<TPrimitive, TValue>
        : PrimitiveTests<TPrimitive, TValue>
        where TPrimitive : Primitive<TValue>
        where TValue : class
    {
        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: should throw if value is null")]
        public void ShouldThrowIfValueIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => CreatePrimitive(null));
        }
    }
}
