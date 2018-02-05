namespace Lifestyle.Planning.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.ReadModels;
    using Lifestyle.Shared;

    public sealed class ProjectQuery : IProjectQuery
    {
        private readonly LifestylePlanningDbContext _context;

        public ProjectQuery(LifestylePlanningDbContext context)
        {
            Guard.ThrowIfNull(context, nameof(context));

            _context = context;
        }
        public IEnumerable<ProjectInfo> FindAll()
        {
            return _context.Projects
                .Where(p => !p.IsArchived)
                .Select(p => new ProjectInfo
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Tasks = p.Tasks.Select(t => new TaskInfo
                    {
                        TaskId = t.TaskId,
                        Name = t.Name
                    })
                }).ToList();
        }

        public ProjectInfo FindById(Guid projectId)
        {
            var dao = _context.Projects
                .SingleOrDefault(p => !p.IsArchived && p.ProjectId == projectId);

            return new ProjectInfo
            {
                ProjectId = dao.ProjectId,
                Name = dao.Name,
                Tasks = dao.Tasks.Select(t => new TaskInfo
                {
                    TaskId = t.TaskId,
                    Name = t.Name
                })
            };
        }
    }
}
