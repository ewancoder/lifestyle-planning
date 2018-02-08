namespace Lifestyle.Planning.Domain.Tests
{
    using Shared.Tests;
    using System;
    using Xunit;

    [Trait("Category", "Project identity")]
    public class ProjectIdentityTests : IdentityTests<ProjectId, Guid>
    {
        private readonly Guid _same = Guid.NewGuid();
        private readonly Guid _another = Guid.NewGuid();

        protected override Guid SameValue() => _same;
        protected override Guid AnotherValue() => _another;
        protected override ProjectId CreateIdentity(Guid value)
            => new ProjectId(value);

        [Fact(DisplayName = "Should create new")]
        public void ShouldCreateNew()
        {
            Assert.NotEqual(Guid.Empty, ProjectId.New().Value);
        }
    }
}
