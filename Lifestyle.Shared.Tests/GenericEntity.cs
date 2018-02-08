namespace Lifestyle.Shared.Tests
{
    public class GenericEntity<TId> : Entity<TId>
    {
        public GenericEntity(TId identity)
        {
            _identity = identity;
        }

        private readonly TId _identity;

        protected override TId GetIdentity()
        {
            return _identity;
        }
    }
}
