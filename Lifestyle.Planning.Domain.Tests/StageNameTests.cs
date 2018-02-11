namespace Lifestyle.Planning.Domain.Tests
{
    using Xunit;

    [Trait("Category", "Stage name")]
    public class StageNameTests : NameTests<StageName>
    {
        protected override int MaxLength() => 100;
        protected override StageName CreatePrimitive(string value)
            => new StageName(value);
    }
}
