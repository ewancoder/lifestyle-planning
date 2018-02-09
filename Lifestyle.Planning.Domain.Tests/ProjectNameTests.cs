namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using Xbehave;
    using Xunit;
    using Shared.Tests;

    [Trait("Category", "Project name")]
    public class ProjectNameTests : PrimitiveTests<ProjectName, string>
    {
        protected override string SameValue() => "same value";
        protected override string AnotherValue() => "another value";
        protected override ProjectName CreatePrimitive(string value)
            => new ProjectName(value);

        [Scenario(DisplayName = "Can't be empty")]
        public void CanNotBeEmpty(string name, ProjectName projectName, Exception exception)
        {
            "Given name"
                .x(() => name = string.Empty);

            "When I create project name"
                .x(() => exception = Record.Exception(() => new ProjectName(name)));

            "Then ArgumentException is thrown"
                .x(() => Assert.IsType<ArgumentException>(exception));
        }

        [Scenario(DisplayName = "Can't exceed 100 characters length")]
        public void CanNotExceed100CharactersLength(string name, ProjectName projectName, Exception exception)
        {
            "Given name that is 101 characters long"
                .x(() => name = new string('f', 101));

            "When I create project name"
                .x(() => exception = Record.Exception(() => new ProjectName(name)));

            "Then ArgumentException is thrown"
                .x(() => Assert.IsType<ArgumentException>(exception));
        }

        [Scenario(DisplayName = "Can be 1 to 100 characters long")]
        [MemberData(nameof(CanBe1To100CharactersLongData))]
        public void CanBe1To100CharactersLong(string name, ProjectName projectName)
        {
            $"Given name in between 1 and 100 characters long: {name}".x(() => { });

            "When I create project name"
                .x(() => projectName = new ProjectName(name));

            "Then name should match project name value"
                .x(() => Assert.Equal(name, projectName.Value));
        }

        public static object[][] CanBe1To100CharactersLongData => new object[][]
        {
            new object[] { "a" },
            new object[] { "abc" },
            new object[] { new string('f', 99) },
            new object[] { new string('f', 100) }
        };

        [Scenario(DisplayName = "Should not accept null")]
        public void ShouldNotAcceptNull(string name, Exception exception)
        {
            "Given name is null".x(() => name = null);

            "When I create project name"
                .x(() => exception = Record.Exception(() => new ProjectName(name)));

            $"Then {nameof(ArgumentNullException)} should be thrown"
                .x(() => Assert.IsType<ArgumentNullException>(exception));
        }
    }
}
