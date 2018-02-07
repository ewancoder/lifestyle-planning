namespace Lifestyle.Planning.Domain.Tests
{
    using Lifestyle.Shared.Tests;
    using Xunit;

    [Trait("Category", "Project entity")]
    public sealed class ProjectEntityTests : EntityTests<Project, ProjectId>
    {
        private readonly ProjectId _sameId = ProjectId.New();
        private readonly ProjectId _anotherId = ProjectId.New();

        protected override ProjectId SameIdentity() => _sameId;
        protected override ProjectId AnotherIdentity() => _anotherId;
        protected override Project CreateEntity(ProjectId identity)
            => new Project(new Project.State { ProjectId = identity });
    }
}
