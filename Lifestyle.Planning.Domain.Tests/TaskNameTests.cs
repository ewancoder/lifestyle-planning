namespace Lifestyle.Planning.Domain.Tests
{
    using Xunit;

    [Trait("Category", "Task name")]
    public class TaskNameTests : NameTests<TaskName>
    {
        protected override int MaxLength() => 100;
        protected override TaskName CreatePrimitive(string value)
            => new TaskName(value);
    }
}
