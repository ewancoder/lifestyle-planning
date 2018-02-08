namespace Lifestyle.Planning.Domain.Tests
{
    using Lifestyle.Shared.Tests;
    using Xunit;

    [Trait("Category", "Task entity")]
    public sealed class TaskEntityTests : EntityTests<Task, TaskId>
    {
        private readonly TaskId _sameId = TaskId.New();
        private readonly TaskId _anotherId = TaskId.New();

        protected override TaskId SameIdentity() => _sameId;
        protected override TaskId AnotherIdentity() => _anotherId;
        protected override Task CreateEntity(TaskId identity)
            => new Task(new Task.State { TaskId = identity });
    }
}
