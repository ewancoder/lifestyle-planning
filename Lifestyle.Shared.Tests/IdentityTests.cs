namespace Lifestyle.Shared.Tests
{
    using System;
    using Xunit;

    public abstract class IdentityTests<TIdentity, TValue>
        where TIdentity : Identity<TValue>
    {
        [Fact(DisplayName = "Shared: should throw if value has default value")]
        public void ShouldThrowIfValueHasDefaultValue()
        {
            Assert.Throws<ArgumentException>(() => CreateIdentity(default(TValue)));
        }

        [Fact(DisplayName = "Shared: should set value")]
        public void ShouldSetValue()
        {
            var value = SameValue();
            var Identity = CreateIdentity(value);

            Assert.Equal(value, Identity.Value);
        }

        [Fact(DisplayName = "Shared: hash code test")]
        public void ShouldPassHashCodeTest()
        {
            var valueA = SameValue();
            var valueB = AnotherValue();
            var identityA1 = CreateIdentity(valueA);
            var identityA2 = CreateIdentity(valueA);
            var identityB1 = CreateIdentity(valueB);

            Assert.Equal(identityA1, identityA2);
            Assert.Equal(identityA1.GetHashCode(), identityA2.GetHashCode());

            Assert.NotEqual(identityA1, identityB1);
            Assert.NotEqual(identityA1.GetHashCode(), identityB1.GetHashCode());
        }

        [Fact(DisplayName = "Shared: equals test")]
        public void ShouldPassEqualsTest()
        {
            var value = SameValue();
            var identity = CreateIdentity(value);
            var sameIdentity = CreateIdentity(value);
            var anotherIdentity = new GenericIdentity<TValue>(value);

            Assert.False(identity.Equals((object)null));
            Assert.True(identity.Equals((object)identity));
            Assert.False(identity.Equals((object)anotherIdentity));
            Assert.True(identity.Equals((object)sameIdentity));
            Assert.False(identity.Equals((object)CreateIdentity(AnotherValue())));
        }

        [Fact(DisplayName = "Shared: generic equals test")]
        public void ShouldPassGenericEqualsTest()
        {
            var value = SameValue();
            var identity = CreateIdentity(value);
            var sameIdentity = CreateIdentity(value);
            var anotherIdentity = new GenericIdentity<TValue>(value);

            GenericIdentity<TValue> nullIdentity = null;
            Assert.False(identity.Equals(nullIdentity));
            Assert.True(identity.Equals(identity));
            Assert.False(identity.Equals(anotherIdentity));
            Assert.True(identity.Equals(sameIdentity));
            Assert.False(identity.Equals(CreateIdentity(AnotherValue())));
        }

        [Fact(DisplayName = "Shared: should return value when converting to string")]
        public void ShouldReturnValueWhenConvertingToString()
        {
            var value = SameValue();
            var identity = CreateIdentity(value);

            Assert.Equal(value.ToString(), identity.ToString());
        }

        [Fact(DisplayName = "Shared: should not match different types")]
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
