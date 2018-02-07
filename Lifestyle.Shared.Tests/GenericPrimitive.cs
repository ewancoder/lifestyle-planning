namespace Lifestyle.Shared.Tests
{
    public class GenericPrimitive<TValue> : Primitive<TValue>
    {
        public GenericPrimitive(TValue value) : base(value)
        {
        }
    }
}
