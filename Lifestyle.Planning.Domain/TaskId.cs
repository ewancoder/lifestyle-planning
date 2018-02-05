namespace Lifestyle.Planning.Domain
{
    using System;
    using Shared;

    public sealed class TaskId : Identity<Guid>
    {
        public TaskId(Guid value) : base(value)
        {
        }

        public static TaskId New() => new TaskId(Guid.NewGuid());
    }
}
