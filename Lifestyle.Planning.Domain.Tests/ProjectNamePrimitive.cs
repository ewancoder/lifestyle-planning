namespace Lifestyle.Planning.Domain.Tests
{
    using Shared.Tests;
    using Xunit;

    [Trait("Category", "Project name primitive")]
    public class ProjectNamePrimitive : PrimitiveTests<ProjectName, string>
    {
        protected override string SameValue() => "same value";
        protected override string AnotherValue() => "another value";
        protected override ProjectName CreatePrimitive(string value)
            => new ProjectName(value);
    }
}
