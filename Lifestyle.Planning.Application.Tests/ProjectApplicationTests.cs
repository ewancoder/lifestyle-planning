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

    [Trait("Category", "Project application")]
    public class ProjectApplicationTests : TestBase
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly ProjectApplication _sut;
        private Project _savedProject;

        public ProjectApplicationTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _sut = new ProjectApplication(_projectRepositoryMock.Object);

            _projectRepositoryMock
                .Setup(x => x.Save(It.IsAny<Project>()))
                .Callback<Project>(x => _savedProject = x);
        }

        [Scenario(DisplayName = "Should create project")]
        public void ShouldCreateProject(string name, ProjectId nextIdentity, Guid resultIdentity)
        {
            "Given name"
                .x(() => name = "project name");

            "And project repository that returns next identity".x(() =>
            {
                nextIdentity = ProjectId.New();

                _projectRepositoryMock
                    .Setup(x => x.GetNextIdentity())
                    .Returns(nextIdentity);
            });

            "When I create project"
                .x(() => resultIdentity = _sut.CreateProject(name));

            "Then project repository should save the project"
                .x(() => _projectRepositoryMock.Verify(x => x.Save(It.IsAny<Project>())));

            "And project identity should be returned"
                .x(() => Assert.Equal(nextIdentity.Value, resultIdentity));

            "And saved project should have valid name and identity".x(() =>
            {
                Assert.Equal(nextIdentity, _savedProject.GetState().ProjectId);
                Assert.Equal(name, _savedProject.GetState().Name.Value);
            });
        }

        [Scenario(DisplayName = "Can't rename nonexistent project")]
        public void CanNotRenameNonexistentProject(Guid projectId, string newName, Exception exception)
        {
            "Given project identity"
                .x(() => projectId = Guid.NewGuid());

            "And name"
                .x(() => newName = "new project name");

            "And nonexistent project".x(() =>
            {
                _projectRepositoryMock
                    .Setup(x => x.FindById(new ProjectId(projectId)))
                    .Returns<Project>(null);
            });

            "When I rename project"
                .x(() => exception = Record.Exception(() => _sut.RenameProject(projectId, newName)));

            $"Then {nameof(InvalidOperationException)} should be thrown"
                .x(() => Assert.IsType<InvalidOperationException>(exception));
        }

        [Scenario(DisplayName = "Should rename project")]
        public void ShouldRenameProject(Guid projectId, string newName, Project project)
        {
            "Given project identity"
                .x(() => projectId = Guid.NewGuid());

            "And name"
                .x(() => newName = "new project name");

            "And existing project with another name".x(() =>
            {
                project = Fixture.Project();
                Assert.NotEqual(newName, project.GetState().Name.Value);

                _projectRepositoryMock
                    .Setup(x => x.FindById(new ProjectId(projectId)))
                    .Returns(project);
            });

            "When I rename project"
                .x(() => _sut.RenameProject(projectId, newName));

            "Then project repository should save the project"
                .x(() => _projectRepositoryMock.Verify(x => x.Save(project)));

            "And saved project should have new name"
                .x(() => Assert.Equal(newName, _savedProject.GetState().Name.Value));
        }

        [Scenario(DisplayName = "Can't archive nonexistent project")]
        public void CanNotArchiveNonexistentProject(Guid projectId, Exception exception)
        {
            "Given project identity"
                .x(() => projectId = Guid.NewGuid());

            "And nonexistent project".x(() =>
            {
                _projectRepositoryMock
                    .Setup(x => x.FindById(new ProjectId(projectId)))
                    .Returns<Project>(null);
            });

            "When I archive project"
                .x(() => exception = Record.Exception(() => _sut.ArchiveProject(projectId)));

            $"Then {nameof(InvalidOperationException)} should be thrown"
                .x(() => Assert.IsType<InvalidOperationException>(exception));
        }

        [Scenario(DisplayName = "Should archive project")]
        public void ShouldArchiveProject(Guid projectId, Project project)
        {
            "Given project identity"
                .x(() => projectId = Guid.NewGuid());

            "And existing non-archived project".x(() =>
            {
                project = Fixture.Project();
                Assert.False(project.GetState().IsArchived);

                _projectRepositoryMock
                    .Setup(x => x.FindById(new ProjectId(projectId)))
                    .Returns(project);
            });

            "When I archive project"
                .x(() => _sut.ArchiveProject(projectId));

            "Then project repository should save the project"
                .x(() => _projectRepositoryMock.Verify(x => x.Save(project)));

            "And saved project should be archived"
                .x(() => Assert.True(_savedProject.GetState().IsArchived));
        }

        protected override IEnumerable<Action> ShouldThrowNullActions()
        {
            return new Action[]
            {
                () => new ProjectApplication(null)
            };
        }
    }
}
