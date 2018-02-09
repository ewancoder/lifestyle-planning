namespace Lifestyle.Shared.Tests
{
    using Xunit;

    public abstract class EntityTests<TEntity, TId> : TestBase
        where TEntity : Entity<TId>
    {
        [Fact(DisplayName = "Hash code test")]
        public void ShouldPassHashCodeTest()
        {
            var identityA = SameIdentity();
            var identityB = AnotherIdentity();
            var entityA1 = CreateEntity(identityA);
            var entityA2 = CreateEntity(identityA);
            var entityB1 = CreateEntity(identityB);

            Assert.Equal(entityA1, entityA2);
            Assert.Equal(entityA1.GetHashCode(), entityA2.GetHashCode());

            Assert.NotEqual(entityA1, entityB1);
            Assert.NotEqual(entityA1.GetHashCode(), entityB1.GetHashCode());
        }

        [Fact(DisplayName = "Equals test")]
        public void ShouldPassEqualsTest()
        {
            var identity = SameIdentity();
            var entity = CreateEntity(identity);
            var sameEntity = CreateEntity(identity);
            var anotherEntity = new GenericEntity<TId>(identity);

            Assert.False(entity.Equals((object)null));
            Assert.True(entity.Equals((object)entity));
            Assert.False(entity.Equals((object)anotherEntity));
            Assert.True(entity.Equals((object)sameEntity));
            Assert.False(entity.Equals((object)CreateEntity(AnotherIdentity())));
        }

        [Fact(DisplayName = "Generic equals test")]
        public void ShouldPassGenericEqualsTest()
        {
            var identity = SameIdentity();
            var entity = CreateEntity(identity);
            var sameEntity = CreateEntity(identity);
            var anotherEntity = new GenericEntity<TId>(identity);

            GenericEntity<TId> nullEntity = null;
            Assert.False(entity.Equals(nullEntity));
            Assert.True(entity.Equals(entity));
            Assert.False(entity.Equals(anotherEntity));
            Assert.True(entity.Equals(sameEntity));
            Assert.False(entity.Equals(CreateEntity(AnotherIdentity())));
        }

        [Fact(DisplayName = "Should not match different types")]
        public void ShouldNotMatchDifferentTypes()
        {
            var identity = SameIdentity();
            Assert.False(CreateEntity(identity).Equals(new GenericIdentity<TId>(identity)));
        }
 
        protected abstract TEntity CreateEntity(TId identity);
        protected abstract TId SameIdentity();
        protected abstract TId AnotherIdentity();
    }
}
