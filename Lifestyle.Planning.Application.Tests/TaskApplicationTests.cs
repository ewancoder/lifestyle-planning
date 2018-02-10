namespace Lifestyle.Planning.Application.Tests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Xbehave;
    using Xunit;
    using Domain;
    using Domain.Tests;
    using Shared.Tests;

    [Trait("Category", "Task application")]
    public class TaskApplicationTests : TestBase
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly TaskApplication _sut;
        private Task _savedTask;

        public TaskApplicationTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _sut = new TaskApplication(_taskRepositoryMock.Object);

            _taskRepositoryMock
                .Setup(x => x.Save(It.IsAny<Task>()))
                .Callback<Task>(x => _savedTask = x);
        }

        [Scenario(DisplayName = "Should create task")]
        public void ShouldCreateTask(string name, Guid projectId, TaskId nextIdentity, Guid resultIdentity)
        {
            "Given name"
                .x(() => name = "task name");

            "And project identity"
                .x(() => projectId = Guid.NewGuid());

            "And task repository that returns next identity".x(() =>
            {
                nextIdentity = TaskId.New();

                _taskRepositoryMock
                    .Setup(x => x.GetNextIdentity())
                    .Returns(nextIdentity);
            });

            "When I create task"
                .x(() => resultIdentity = _sut.CreateTask(projectId, name));

            "Then task repository should save the task"
                .x(() => _taskRepositoryMock.Verify(x => x.Save(It.IsAny<Task>())));

            "And task identity should be returned"
                .x(() => Assert.Equal(nextIdentity.Value, resultIdentity));

            "And saved task should have valid name, identity and project identity".x(() =>
            {
                Assert.Equal(projectId, _savedTask.GetState().ProjectId.Value);
                Assert.Equal(nextIdentity, _savedTask.GetState().TaskId);
                Assert.Equal(name, _savedTask.GetState().Name.Value);
            });
        }

        [Scenario(DisplayName = "Can't rename nonexistent task")]
        public void CanNotRenameNonexistentTask(Guid taskId, string newName, Exception exception)
        {
            "Given task identity"
                .x(() => taskId = Guid.NewGuid());

            "And name"
                .x(() => newName = "new task name");

            "And nonexistent task".x(() =>
            {
                _taskRepositoryMock
                    .Setup(x => x.FindById(new TaskId(taskId)))
                    .Returns<Task>(null);
            });

            "When I rename task"
                .x(() => exception = Record.Exception(() => _sut.RenameTask(taskId, newName)));

            $"Then {nameof(InvalidOperationException)} should be thrown"
                .x(() => Assert.IsType<InvalidOperationException>(exception));
        }

        [Scenario(DisplayName = "Should rename task")]
        public void ShouldRenameTask(Guid taskId, string newName, Task task)
        {
            "Given task identity"
                .x(() => taskId = Guid.NewGuid());

            "And name"
                .x(() => newName = "new task name");

            "And existing task with another name".x(() =>
            {
                task = Fixture.Task();
                Assert.NotEqual(newName, task.GetState().Name.Value);

                _taskRepositoryMock
                    .Setup(x => x.FindById(new TaskId(taskId)))
                    .Returns(task);
            });

            "When I rename task"
                .x(() => _sut.RenameTask(taskId, newName));

            "Then task repository should save the task"
                .x(() => _taskRepositoryMock.Verify(x => x.Save(task)));

            "And saved task should have new name"
                .x(() => Assert.Equal(newName, _savedTask.GetState().Name.Value));
        }

        [Scenario(DisplayName = "Can't archive nonexistent task")]
        public void CanNotArchiveNonexistentTask(Guid taskId, Exception exception)
        {
            "Given task identity"
                .x(() => taskId = Guid.NewGuid());

            "And nonexistent task".x(() =>
            {
                _taskRepositoryMock
                    .Setup(x => x.FindById(new TaskId(taskId)))
                    .Returns<Task>(null);
            });

            "When I archive task"
                .x(() => exception = Record.Exception(() => _sut.ArchiveTask(taskId)));

            $"Then {nameof(InvalidOperationException)} should be thrown"
                .x(() => Assert.IsType<InvalidOperationException>(exception));
        }

        [Scenario(DisplayName = "Should archive task")]
        public void ShouldArchiveTask(Guid taskId, Task task)
        {
            "Given task identity"
                .x(() => taskId = Guid.NewGuid());

            "And existing non-archived task".x(() =>
            {
                task = Fixture.Task();
                Assert.False(task.GetState().IsArchived);

                _taskRepositoryMock
                    .Setup(x => x.FindById(new TaskId(taskId)))
                    .Returns(task);
            });

            "When I archive task"
                .x(() => _sut.ArchiveTask(taskId));

            "Then task repository should save the task"
                .x(() => _taskRepositoryMock.Verify(x => x.Save(task)));

            "And saved task should be archived"
                .x(() => Assert.True(_savedTask.GetState().IsArchived));
        }

        protected override IEnumerable<Action> ShouldThrowNullActions()
        {
            return new Action[]
            {
                () => new TaskApplication(null)
            };
        }
    }
}
