namespace Lifestyle.Planning.Domain
{
    using System;
    using Shared;

    public sealed class Task : AggregateRoot<TaskId>
    {
        public sealed class State
        {
            public ProjectId ProjectId { get; set; }
            public StageId StageId { get; set; }
            public TaskId TaskId { get; set; }
            public TaskName Name { get; set; }
            public bool IsArchived { get; set; }

            internal State Clone()
            {
                return new State
                {
                    ProjectId = ProjectId,
                    StageId = StageId,
                    TaskId = TaskId,
                    Name = Name,
                    IsArchived = IsArchived
                };
            }
        }

        private readonly State _state;

        // Project actuality/validation is not performed.
        public Task(TaskId taskId, ProjectId projectId, StageId stageId, TaskName name)
        {
            Guard.ThrowIfNull(taskId, nameof(taskId));
            Guard.ThrowIfNull(projectId, nameof(projectId));
            Guard.ThrowIfNull(stageId, nameof(stageId));
            Guard.ThrowIfNull(name, nameof(name));

            _state = new State
            {
                TaskId = taskId,
                ProjectId = projectId,
                StageId = stageId,
                Name = name
            };
        }

        public Task(State state)
        {
            Guard.ThrowIfNull(state, nameof(state));

            _state = state.Clone();
        }

        public State GetState()
        {
            return _state.Clone();
        }

        public void Rename(TaskName name)
        {
            Guard.ThrowIfNull(name, nameof(name));

            _state.Name = name;
        }

        public void Archive()
        {
            if (_state.IsArchived)
                throw new InvalidOperationException("Task is already archived.");

            _state.IsArchived = true;
        }

        protected override TaskId GetIdentity()
        {
            return _state.TaskId;
        }
    }
}
