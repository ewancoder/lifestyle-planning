namespace Lifestyle.Planning.Domain.Tests
{
    using Xunit;

    [Trait("Category", "Project name")]
    public class ProjectNameTests : NameTests<ProjectName>
    {
        protected override int MaxLength() => 100;
        protected override ProjectName CreatePrimitive(string value)
            => new ProjectName(value);
    }
}
