namespace Lifestyle.Planning.Domain
{
    using System;
    using Shared;

    public sealed class ProjectId : Identity<Guid>
    {
        public ProjectId(Guid value) : base(value)
        {
        }

        public static ProjectId New() => new ProjectId(Guid.NewGuid());
    }
}
