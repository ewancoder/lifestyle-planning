namespace Lifestyle.Planning.Domain.Tests
{
    using Shared.Tests;
    using System;
    using Xunit;

    [Trait("Category", "Task identity")]
    public class TaskIdentityTests : IdentityTests<TaskId, Guid>
    {
        private readonly Guid _same = Guid.NewGuid();
        private readonly Guid _another = Guid.NewGuid();

        protected override Guid SameValue() => _same;
        protected override Guid AnotherValue() => _another;
        protected override TaskId CreateIdentity(Guid value)
            => new TaskId(value);

        [Fact(DisplayName = "Should create new")]
        public void ShouldCreateNew()
        {
            Assert.NotEqual(Guid.Empty, TaskId.New().Value);
        }
    }
}
