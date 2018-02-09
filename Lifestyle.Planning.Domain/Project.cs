namespace Lifestyle.Planning.Domain
{
    using System;
    using System.Collections.Generic;
    using Shared;

    public sealed class Project : AggregateRoot<ProjectId>
    {
        public sealed class State
        {
            public ProjectId ProjectId { get; set; }
            public ProjectName Name { get; set; }
            public bool IsArchived { get; set; }

            internal State Clone()
            {
                return new State
                {
                    ProjectId = ProjectId,
                    Name = Name,
                    IsArchived = IsArchived
                };
            }
        }

        private readonly State _state;

        public Project(ProjectId projectId, ProjectName name)
        {
            Guard.ThrowIfNull(projectId, nameof(projectId));
            Guard.ThrowIfNull(name, nameof(name));

            _state = new State
            {
                ProjectId = projectId,
                Name = name
            };
        }

        public Project(State state)
        {
            Guard.ThrowIfNull(state, nameof(state));

            _state = state.Clone();
        }

        public State GetState()
        {
            return _state.Clone();
        }

        public void Rename(ProjectName name)
        {
            Guard.ThrowIfNull(name, nameof(name));

            _state.Name = name;
        }

        public void Archive()
        {
            if (_state.IsArchived)
                throw new InvalidOperationException("Project is already archived.");

            _state.IsArchived = true;
        }

        protected override ProjectId GetIdentity()
        {
            return _state.ProjectId;
        }
    }
}
