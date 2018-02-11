namespace Lifestyle.Planning.Domain.Tests
{
    using Xunit;

    [Trait("Category", "Name")]
    public class ConcreteNameTests : NameTests<TestName>
    {
        protected override int MaxLength() => TestName.MaxLength;
        protected override TestName CreatePrimitive(string value)
            => new TestName(value);
    }
}
