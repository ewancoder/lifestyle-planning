namespace Lifestyle.Shared.Tests
{
    using System;
    using Xunit;

    public abstract class IdentityTests<TIdentity, TValue>
        where TIdentity : Identity<TValue>
    {
        [Fact(DisplayName = "Should throw if value has default value")]
        public void ShouldThrowIfValueIsNull()
        {
            Assert.Throws<ArgumentException>(() => CreateIdentity(default(TValue)));
        }

        [Fact(DisplayName = "Should set value")]
        public void ShouldSetValue()
        {
            var value = SameValue();
            var Identity = CreateIdentity(value);

            Assert.Equal(value, Identity.Value);
        }

        [Fact(DisplayName = "Hash code test")]
        public void ShouldPassHashCodeTest()
        {
            var valueA = SameValue();
            var valueB = AnotherValue();
            var IdentityA1 = CreateIdentity(valueA);
            var IdentityA2 = CreateIdentity(valueA);
            var IdentityB1 = CreateIdentity(valueB);

            Assert.Equal(IdentityA1, IdentityA2);
            Assert.Equal(IdentityA1.GetHashCode(), IdentityA2.GetHashCode());

            Assert.NotEqual(IdentityA1, IdentityB1);
            Assert.NotEqual(IdentityA1.GetHashCode(), IdentityB1.GetHashCode());
        }

        [Fact(DisplayName = "Equals test")]
        public void ShouldPassEqualsTest()
        {
            var value = SameValue();
            var Identity = CreateIdentity(value);
            var sameIdentity = CreateIdentity(value);
            var anotherIdentity = new GenericIdentity<TValue>(value);

            Assert.False(Identity.Equals((object)null));
            Assert.True(Identity.Equals((object)Identity));
            Assert.False(Identity.Equals((object)anotherIdentity));
            Assert.True(Identity.Equals((object)sameIdentity));
            Assert.False(Identity.Equals((object)CreateIdentity(AnotherValue())));
        }

        [Fact(DisplayName = "Generic equals test")]
        public void ShouldPassGenericEqualsTest()
        {
            var value = SameValue();
            var Identity = CreateIdentity(value);
            var sameIdentity = CreateIdentity(value);
            var anotherIdentity = new GenericIdentity<TValue>(value);

            GenericIdentity<TValue> nullIdentity = null;
            Assert.False(Identity.Equals(nullIdentity));
            Assert.True(Identity.Equals(Identity));
            Assert.False(Identity.Equals(anotherIdentity));
            Assert.True(Identity.Equals(sameIdentity));
            Assert.False(Identity.Equals(CreateIdentity(AnotherValue())));
        }

        [Fact(DisplayName = "Should return value when converting to string")]
        public void ShouldReturnValueWhenConvertingToString()
        {
            var value = SameValue();
            var Identity = CreateIdentity(value);

            Assert.Equal(value.ToString(), Identity.ToString());
        }

        [Fact(DisplayName = "Should not match different types")]
        public void ShouldNotMatchDifferentTypes()
        {
            var value = SameValue();
            Assert.False(CreateIdentity(value).Equals(new GenericIdentity<TValue>(value)));
        }
 
        protected abstract TIdentity CreateIdentity(TValue value);
        protected abstract TValue SameValue();
        protected abstract TValue AnotherValue();
    }
}
