namespace Lifestyle.Planning.Domain
{
    using Shared;

    public sealed class StageId : Identity<int>
    {
        public StageId(int value) : base(value)
        {
        }
    }
}
