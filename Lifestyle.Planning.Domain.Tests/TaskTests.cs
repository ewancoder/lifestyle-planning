namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Xbehave;
    using Xunit;
    using Shared.Tests;

    [Trait("Category", "Task")]
    public sealed class TaskTests : EntityTests<Task, TaskId>
    {
        private readonly TaskId _sameId = TaskId.New();
        private readonly TaskId _anotherId = TaskId.New();

        protected override TaskId SameIdentity() => _sameId;
        protected override TaskId AnotherIdentity() => _anotherId;
        protected override Task CreateEntity(TaskId identity)
            => new Task(new Task.State { TaskId = identity });

        protected override IEnumerable<Action> ShouldThrowNullActions()
        {
            var task = Fixture.Task();

            return new Action[]
            {
                () => new Task(null, Fixture.ProjectId(), Fixture.StageId(), Fixture.TaskName()),
                () => new Task(Fixture.TaskId(), null, Fixture.StageId(), Fixture.TaskName()),
                () => new Task(Fixture.TaskId(), Fixture.ProjectId(), null, Fixture.TaskName()),
                () => new Task(Fixture.TaskId(), Fixture.ProjectId(), Fixture.StageId(), null),
                () => new Task(null),
                () => task.Rename(null)
            };
        }

        [Fact(DisplayName = "Should reconstitute")]
        public void ShouldReconstitute()
        {
            var state = Fixture.TaskState();

            var task = new Task(state);

            Fixture.AssertEqual(state, task.GetState());
        }

        [Scenario(DisplayName = "Can rename")]
        public void CanRename(Task task, TaskName name)
        {
            "Given task"
                .x(() => task = Fixture.Task());

            "And another name".x(() =>
            {
                name = new TaskName("another name");
                Assert.NotEqual(task.GetState().Name, name);
            });

            "When I rename task"
                .x(() => task.Rename(name));

            "Then task's name changes"
                .x(() => Assert.Equal(name, task.GetState().Name));
        }

        [Scenario(DisplayName = "Can archive")]
        public void CanArchive(Task task)
        {
            "Given not archived task".x(() =>
            {
                task = Fixture.Task();
                Assert.False(task.GetState().IsArchived);
            });

            "When I archive task"
                .x(() => task.Archive());

            "Then task is archived"
                .x(() => Assert.True(task.GetState().IsArchived));
        }

        [Scenario(DisplayName = "Cannot archive already archived task")]
        public void CannotArchiveAlreadyArchivedTask(Task task, Exception exception)
        {
            "Given already archived task".x(() =>
            {
                task = new Task(new Task.State
                {
                    IsArchived = true
                });
            });

            "When I archive task"
                .x(() => exception = Record.Exception(() => task.Archive()));

            $"Then task throws {nameof(InvalidOperationException)}"
                .x(() => Assert.IsType<InvalidOperationException>(exception));
        }

        [Scenario(DisplayName = "Can create")]
        public void CanCreate(TaskId taskId, ProjectId projectId, StageId stageId, TaskName name, Task task)
        {
            "Given task identity"
                .x(() => taskId = Fixture.TaskId());

            "And project identity"
                .x(() => projectId = Fixture.ProjectId());

            "And stage identity"
                .x(() => stageId = Fixture.StageId());

            "And task name"
                .x(() => name = Fixture.TaskName());

            "When I create task"
                .x(() => task = new Task(taskId, projectId, stageId, name));

            "Then task has appropriate identity, name, project identity and stage identity".x(() =>
            {
                Assert.Equal(taskId, task.GetState().TaskId);
                Assert.Equal(name, task.GetState().Name);
                Assert.Equal(projectId, task.GetState().ProjectId);
                Assert.Equal(stageId, task.GetState().StageId);
            });
        }

        [Scenario(DisplayName = "Should not be archived when created")]
        public void ShouldNotBeArchivedWhenCreated(Task task)
        {
            "When I create task"
                .x(() => task = new Task(Fixture.TaskId(), Fixture.ProjectId(), Fixture.StageId(), Fixture.TaskName()));

            "Then task is not archived"
                .x(() => Assert.False(task.GetState().IsArchived));
        }
    }
}
