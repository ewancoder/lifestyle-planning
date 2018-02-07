namespace Lifestyle.Planning.Domain.Tests
{
    using Shared.Tests;
    using Xunit;

    [Trait("Category", "Task name primitive")]
    public class TaskNamePrimitive : PrimitiveTests<TaskName, string>
    {
        protected override string SameValue() => "same value";
        protected override string AnotherValue() => "another value";
        protected override TaskName CreatePrimitive(string value)
            => new TaskName(value);
    }
}
