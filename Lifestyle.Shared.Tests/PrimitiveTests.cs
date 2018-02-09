namespace Lifestyle.Shared.Tests
{
    using Xunit;

    public abstract class PrimitiveTests<TPrimitive, TValue>
        where TPrimitive : Primitive<TValue>
    {
        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: should set value")]
        public void ShouldSetValue()
        {
            var value = SameValue();
            var primitive = CreatePrimitive(value);

            Assert.Equal(value, primitive.Value);
        }

        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: hash code test")]
        public void ShouldPassHashCodeTest()
        {
            var valueA = SameValue();
            var valueB = AnotherValue();
            var primitiveA1 = CreatePrimitive(valueA);
            var primitiveA2 = CreatePrimitive(valueA);
            var primitiveB1 = CreatePrimitive(valueB);

            Assert.Equal(primitiveA1, primitiveA2);
            Assert.Equal(primitiveA1.GetHashCode(), primitiveA2.GetHashCode());

            Assert.NotEqual(primitiveA1, primitiveB1);
            Assert.NotEqual(primitiveA1.GetHashCode(), primitiveB1.GetHashCode());
        }

        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: equals test")]
        public void ShouldPassEqualsTest()
        {
            var value = SameValue();
            var primitive = CreatePrimitive(value);
            var samePrimitive = CreatePrimitive(value);
            var anotherPrimitive = new GenericPrimitive<TValue>(value);

            Assert.False(primitive.Equals((object)null));
            Assert.True(primitive.Equals((object)primitive));
            Assert.False(primitive.Equals((object)anotherPrimitive));
            Assert.True(primitive.Equals((object)samePrimitive));
            Assert.False(primitive.Equals((object)CreatePrimitive(AnotherValue())));
        }

        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: generic equals test")]
        public void ShouldPassGenericEqualsTest()
        {
            var value = SameValue();
            var primitive = CreatePrimitive(value);
            var samePrimitive = CreatePrimitive(value);
            var anotherPrimitive = new GenericPrimitive<TValue>(value);

            GenericPrimitive<TValue> nullPrimitive = null;
            Assert.False(primitive.Equals(nullPrimitive));
            Assert.True(primitive.Equals(primitive));
            Assert.False(primitive.Equals(anotherPrimitive));
            Assert.True(primitive.Equals(samePrimitive));
            Assert.False(primitive.Equals(CreatePrimitive(AnotherValue())));
        }

        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: should return value when converting to string")]
        public void ShouldReturnValueWhenConvertingToString()
        {
            var value = SameValue();
            var primitive = CreatePrimitive(value);

            Assert.Equal(value.ToString(), primitive.ToString());
        }

        [Trait("Category", "Shared")]
        [Fact(DisplayName = "Shared: should not match different types")]
        public void ShouldNotMatchDifferentTypes()
        {
            var value = SameValue();
            Assert.False(CreatePrimitive(value).Equals(new GenericPrimitive<TValue>(value)));
        }
 
        protected abstract TPrimitive CreatePrimitive(TValue value);
        protected abstract TValue SameValue();
        protected abstract TValue AnotherValue();
    }
}
