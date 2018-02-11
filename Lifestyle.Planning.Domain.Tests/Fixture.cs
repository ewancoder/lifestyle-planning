namespace Lifestyle.Planning.Domain.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public static class Fixture
    {
        public static Project Project()
        {
            return new Project(ProjectId(), ProjectName());
        }

        public static ProjectId ProjectId()
        {
            return Domain.ProjectId.New();
        }

        public static ProjectName ProjectName()
        {
            return new ProjectName("project name");
        }

        public static Project.State ProjectState()
        {
            return new Project.State
            {
                ProjectId = ProjectId(),
                Name = ProjectName(),
                IsArchived = false,
                Stages = new List<Stage> { Stage() }
            };
        }

        public static void AssertEqual(Project.State expected, Project.State actual)
        {
            Assert.Equal(expected.ProjectId, actual.ProjectId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.IsArchived, actual.IsArchived);

            Assert.True(Enumerable.SequenceEqual(expected.Stages, actual.Stages));
            Assert.False(ReferenceEquals(expected.Stages, actual.Stages));
        }

        public static Task Task()
        {
            return new Task(TaskId(), ProjectId(), StageId(), TaskName());
        }

        public static TaskId TaskId()
        {
            return Domain.TaskId.New();
        }

        public static TaskName TaskName()
        {
            return new TaskName("task name");
        }

        public static Task.State TaskState()
        {
            return new Task.State
            {
                TaskId = TaskId(),
                ProjectId = ProjectId(),
                StageId = StageId(),
                Name = TaskName(),
                IsArchived = false
            };
        }

        public static void AssertEqual(Task.State expected, Task.State actual)
        {
            Assert.Equal(expected.TaskId, actual.TaskId);
            Assert.Equal(expected.ProjectId, actual.ProjectId);
            Assert.Equal(expected.StageId, actual.StageId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.IsArchived, actual.IsArchived);
        }

        public static Stage Stage()
        {
            return new Stage(StageId(), StageName());
        }

        public static StageId StageId()
        {
            return new StageId(100);
        }

        public static StageName StageName()
        {
            return new StageName("stage name");
        }
    }
}
