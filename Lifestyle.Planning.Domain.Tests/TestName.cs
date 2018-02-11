namespace Lifestyle.Planning.Domain.Tests
{
    public sealed class TestName : Name
    {
        public static readonly int MaxLength = 3;

        public TestName(string name) : base(name, MaxLength)
        {
        }
    }
}
