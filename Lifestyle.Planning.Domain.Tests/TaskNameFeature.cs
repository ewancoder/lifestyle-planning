namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using Xbehave;
    using Xunit;

    public class TaskNameFeature
    {
        [Trait("Category", "Task name")]
        [Scenario(DisplayName = "Can't be empty")]
        public void CanNotBeEmpty(string name, TaskName taskName, Exception exception)
        {
            "Given name"
                .x(() => name = string.Empty);

            "When I create task name"
                .x(() => exception = Record.Exception(() => new TaskName(name)));

            "Then ArgumentException is thrown"
                .x(() => Assert.IsType<ArgumentException>(exception));
        }

        [Trait("Category", "Task name")]
        [Scenario(DisplayName = "Can't exceed 100 characters length")]
        public void CanNotExceed100CharactersLength(string name, TaskName taskName, Exception exception)
        {
            "Given name that is 101 characters long"
                .x(() => name = new string('f', 101));

            "When I create task name"
                .x(() => exception = Record.Exception(() => new TaskName(name)));

            "Then ArgumentException is thrown"
                .x(() => Assert.IsType<ArgumentException>(exception));
        }

        [Trait("Category", "Task name")]
        [Scenario(DisplayName = "Can be 1 to 100 characters long")]
        [MemberData(nameof(CanBe1To100CharactersLongData))]
        public void CanBe1To100CharactersLong(string name, TaskName taskName)
        {
            $"Given name in between 1 and 100 characters long: {name}".x(() => { });

            "When I create task name"
                .x(() => taskName = new TaskName(name));

            "Then name should match task name value"
                .x(() => Assert.Equal(name, taskName.Value));
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
