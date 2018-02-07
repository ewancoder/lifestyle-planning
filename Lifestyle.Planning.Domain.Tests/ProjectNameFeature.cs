﻿namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using Xbehave;
    using Xunit;

    public class ProjectNameFeature
    {
        [Trait("Category", "Project name")]
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

        [Trait("Category", "Project name")]
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

        [Trait("Category", "Project name")]
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
    }
}