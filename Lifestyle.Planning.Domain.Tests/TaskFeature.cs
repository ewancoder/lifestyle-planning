namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Xbehave;
    using Xunit;

    public sealed class TaskFeature
    {
        [Trait("Category", "Task")]
        [Scenario(DisplayName = "Can rename")]
        public void CanRename(Task task, TaskName name)
        {
            "Given task"
                .x(() => task = TestFixture.Task());

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

        [Trait("Category", "Task")]
        [Scenario(DisplayName = "Can archive")]
        public void CanArchive(Task task)
        {
            "Given not archived task".x(() =>
            {
                task = TestFixture.Task();
                Assert.False(task.GetState().IsArchived);
            });

            "When I archive task"
                .x(() => task.Archive());

            "Then task is archived"
                .x(() => Assert.True(task.GetState().IsArchived));
        }

        [Trait("Category", "Task")]
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

        [Trait("Category", "Task")]
        [Scenario(DisplayName = "Can create")]
        public void CanCreate(TaskId taskId, ProjectId projectId, TaskName name, Task task)
        {
            "Given task identity"
                .x(() => taskId = TestFixture.TaskId());

            "And project identity"
                .x(() => projectId = TestFixture.ProjectId());

            "And task name"
                .x(() => name = TestFixture.TaskName());

            "When I create task"
                .x(() => task = new Task(taskId, projectId, name));

            "Then task has appropriate identity, name and project identity".x(() =>
            {
                Assert.Equal(taskId, task.GetState().TaskId);
                Assert.Equal(name, task.GetState().Name);
                Assert.Equal(projectId, task.GetState().ProjectId);
            });
        }

        [Trait("Category", "Task")]
        [Scenario(DisplayName = "Should not be archived when created")]
        public void ShouldNotBeArchivedWhenCreated(Task task)
        {
            "When I create task"
                .x(() => task = new Task(TestFixture.TaskId(), TestFixture.ProjectId(), TestFixture.TaskName()));

            "Then task is not archived"
                .x(() => Assert.False(task.GetState().IsArchived));
        }

        [Trait("Category", "Task")]
        [Fact(DisplayName = "Should not accept null arguments")]
        public void ShouldNotAcceptNullArguments()
        {
            var task = TestFixture.Task();

            ThrowsNull(new Action[]
            {
                () => new Task(null, TestFixture.ProjectId(), TestFixture.TaskName()),
                () => new Task(TestFixture.TaskId(), null, TestFixture.TaskName()),
                () => new Task(TestFixture.TaskId(), TestFixture.ProjectId(), null),
                () => new Task(null),
                () => task.Rename(null)
            });
        }

        private void ThrowsNull(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
                Assert.Throws<ArgumentNullException>(action);
        }
    }
}
