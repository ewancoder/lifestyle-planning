namespace Lifestyle.Shared.Tests
{
    public class GenericIdentity<TValue> : Identity<TValue>
    {
        public GenericIdentity(TValue value) : base(value)
        {
        }
    }
}
