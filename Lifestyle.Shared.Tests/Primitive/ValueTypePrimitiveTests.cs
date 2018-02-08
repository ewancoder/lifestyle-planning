namespace Lifestyle.Shared.Tests.Primitive
{
    using Xunit;

    [Trait("Category", "Primitive")]
    public class ValueTypePrimitiveTests : PrimitiveTests<ValueTestPrimitive, int>
    {
        protected override int SameValue() => 10;
        protected override int AnotherValue() => 20;
        protected override ValueTestPrimitive CreatePrimitive(int value)
            => new ValueTestPrimitive(value);
    }
}
