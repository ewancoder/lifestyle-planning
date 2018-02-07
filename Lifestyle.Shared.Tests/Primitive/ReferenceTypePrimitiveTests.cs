namespace Lifestyle.Shared.Tests.Primitive
{
    using Xunit;

    [Trait("Category", "Primitive")]
    public class ReferenceTypePrimitiveTests : ReferencePrimitiveTests<ReferenceTestPrimitive, string>
    {
        protected override string SameValue() => "same value";
        protected override string AnotherValue() => "another value";
        protected override ReferenceTestPrimitive CreatePrimitive(string value)
            => new ReferenceTestPrimitive(value);
    }
}
