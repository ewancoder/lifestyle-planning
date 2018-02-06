﻿namespace Lifestyle.Planning.Domain.Tests
{
    internal static class TestFixture
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
    }
}
