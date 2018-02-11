namespace Lifestyle.Planning.Domain.Tests
{
    using System;
    using Xbehave;
    using Xunit;
    using Shared.Tests;

    [Trait("Category", "Name")]
    public abstract class NameTests<TName> : ReferencePrimitiveTests<TName, string>
        where TName : Name
    {
        protected override string SameValue() => new string('s', Math.Min(5, MaxLength()));
        protected override string AnotherValue() => new string('a', Math.Min(5, MaxLength()));
        protected abstract int MaxLength();

        [Scenario(DisplayName = "Can't be empty")]
        public void CanNotBeEmpty(string nameValue, TName name, Exception exception)
        {
            "Given name value"
                .x(() => nameValue = string.Empty);

            "When I create name"
                .x(() => exception = Record.Exception(() => CreatePrimitive(nameValue)));

            "Then ArgumentException is thrown"
                .x(() => Assert.IsType<ArgumentException>(exception));
        }

        [Scenario(DisplayName = "Can't exceed max length")]
        public void CanNotExceedMaxLength(string nameValue, TName name, Exception exception)
        {
            $"Given name value that is {MaxLength() + 1} characters long"
                .x(() => nameValue = new string('f', MaxLength() + 1));

            "When I create name"
                .x(() => exception = Record.Exception(() => CreatePrimitive(nameValue)));

            "Then ArgumentException is thrown"
                .x(() => Assert.IsType<ArgumentException>(exception));
        }

        [Scenario(DisplayName = "Can be 1 character long")]
        public void CanBe1CharacterLong(string nameValue, TName name)
        {
            $"Given name value is 1 character long: {nameValue}"
                .x(() => nameValue = "a");

            "When I create name"
                .x(() => name = CreatePrimitive(nameValue));

            "Then name should match name value"
                .x(() => Assert.Equal(nameValue, name.Value));
        }

        [Scenario(DisplayName = "Can be max characters long")]
        public void CanBeMaxCharactersLong(string nameValue, TName name)
        {
            $"Given name value is {MaxLength()} characters long: {nameValue}"
                .x(() => nameValue = new string('f', MaxLength()));

            "When I create name"
                .x(() => name = CreatePrimitive(nameValue));

            "Then name should match name value"
                .x(() => Assert.Equal(nameValue, name.Value));
        }
    }
}
