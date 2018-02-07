namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Xbehave;
    using Xunit;

    [Trait("Category", "Project")]
    public sealed class ProjectFeature
    {
        [Scenario(DisplayName = "Can rename")]
        public void CanRename(Project project, ProjectName name)
        {
            "Given project"
                .x(() => project = TestFixture.Project());

            "And another name".x(() =>
            {
                name = new ProjectName("another name");
                Assert.NotEqual(project.GetState().Name, name);
            });

            "When I rename project"
                .x(() => project.Rename(name));

            "Then project's name changes"
                .x(() => Assert.Equal(name, project.GetState().Name));
        }

        [Scenario(DisplayName = "Can archive")]
        public void CanArchive(Project project)
        {
            "Given not archived project".x(() =>
            {
                project = TestFixture.Project();
                Assert.False(project.GetState().IsArchived);
            });

            "When I archive project"
                .x(() => project.Archive());

            "Then project is archived"
                .x(() => Assert.True(project.GetState().IsArchived));
        }

        [Scenario(DisplayName = "Cannot archive already archived project")]
        public void CannotArchiveAlreadyArchivedProject(Project project, Exception exception)
        {
            "Given already archived project".x(() =>
            {
                project = new Project(new Project.State
                {
                    IsArchived = true
                });
            });

            "When I archive project"
                .x(() => exception = Record.Exception(() => project.Archive()));

            $"Then project throws {nameof(InvalidOperationException)}"
                .x(() => Assert.IsType<InvalidOperationException>(exception));
        }

        [Scenario(DisplayName = "Can create")]
        public void CanCreate(ProjectId projectId, ProjectName name, Project project)
        {
            "Given project identity"
                .x(() => projectId = TestFixture.ProjectId());

            "And project name"
                .x(() => name = TestFixture.ProjectName());

            "When I create project"
                .x(() => project = new Project(projectId, name));

            "Then project has appropriate identity and name".x(() =>
            {
                Assert.Equal(projectId, project.GetState().ProjectId);
                Assert.Equal(name, project.GetState().Name);
            });
        }

        [Scenario(DisplayName = "Should not be archived when created")]
        public void ShouldNotBeArchivedWhenCreated(Project project)
        {
            "When I create project"
                .x(() => project = new Project(TestFixture.ProjectId(), TestFixture.ProjectName()));

            "Then project is not archived"
                .x(() => Assert.False(project.GetState().IsArchived));
        }

        [Fact(DisplayName = "Should not accept null arguments")]
        public void ShouldNotAcceptNullArguments()
        {
            var project = TestFixture.Project();

            ThrowsNull(new Action[]
            {
                () => new Project(null, TestFixture.ProjectName()),
                () => new Project(TestFixture.ProjectId(), null),
                () => new Project(null),
                () => project.Rename(null)
            });
        }

        private void ThrowsNull(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
                Assert.Throws<ArgumentNullException>(action);
        }
    }
}
