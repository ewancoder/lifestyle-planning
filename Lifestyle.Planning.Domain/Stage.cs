namespace Lifestyle.Planning.Domain
{
    using Shared;

    public sealed class Stage : Entity<StageId>
    {
        public Stage(StageId stageId, StageName name)
        {
            Guard.ThrowIfNull(stageId, nameof(stageId));
            Guard.ThrowIfNull(name, nameof(name));

            StageId = stageId;
            Name = name;
        }

        public StageId StageId { get; }
        public StageName Name { get; }

        protected override StageId GetIdentity() => StageId;
    }
}
