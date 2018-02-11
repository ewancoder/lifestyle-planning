namespace Lifestyle.Planning.Domain.Tests
{
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
                IsArchived = false
            };
        }

        public static void AssertEqual(Project.State expected, Project.State actual)
        {
            Assert.Equal(expected.ProjectId, actual.ProjectId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.IsArchived, actual.IsArchived);
        }

        public static Task Task()
        {
            return new Task(TaskId(), ProjectId(), TaskName());
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
                Name = TaskName(),
                IsArchived = false
            };
        }

        public static void AssertEqual(Task.State expected, Task.State actual)
        {
            Assert.Equal(expected.TaskId, actual.TaskId);
            Assert.Equal(expected.ProjectId, actual.ProjectId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.IsArchived, actual.IsArchived);
        }
    }
}
