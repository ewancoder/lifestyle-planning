namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using Xbehave;
    using Xunit;
    using Shared.Tests;
    using System.Collections.Generic;

    [Trait("Category", "Project")]
    public sealed class ProjectTests : EntityTests<Project, ProjectId>
    {
        private readonly ProjectId _sameId = ProjectId.New();
        private readonly ProjectId _anotherId = ProjectId.New();

        protected override ProjectId SameIdentity() => _sameId;
        protected override ProjectId AnotherIdentity() => _anotherId;
        protected override Project CreateEntity(ProjectId identity)
            => new Project(new Project.State { ProjectId = identity });

        protected override IEnumerable<Action> ShouldThrowNullActions()
        {
            var project = Fixture.Project();

            return new Action[]
            {
                () => new Project(null, Fixture.ProjectName()),
                () => new Project(Fixture.ProjectId(), null),
                () => new Project(null),
                () => project.Rename(null)
            };
        }

        [Fact(DisplayName = "Should reconstitute")]
        public void ShouldReconstitute()
        {
            var state = Fixture.ProjectState();

            var project = new Project(state);

            Fixture.AssertEqual(state, project.GetState());
        }

        [Scenario(DisplayName = "Can rename")]
        public void CanRename(Project project, ProjectName name)
        {
            "Given project"
                .x(() => project = Fixture.Project());

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
                project = Fixture.Project();
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
                .x(() => projectId = Fixture.ProjectId());

            "And project name"
                .x(() => name = Fixture.ProjectName());

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
                .x(() => project = new Project(Fixture.ProjectId(), Fixture.ProjectName()));

            "Then project is not archived"
                .x(() => Assert.False(project.GetState().IsArchived));
        }
    }
}
