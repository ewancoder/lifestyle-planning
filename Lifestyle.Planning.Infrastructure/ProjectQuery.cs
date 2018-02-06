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
        public IEnumerable<ProjectInfo> FindAllInfo()
        {
            return _context.Projects
                .Where(p => !p.IsArchived)
                .Select(p => new ProjectInfo
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name
                }).ToList();
        }

        public ProjectDetails FindDetailsById(Guid projectId)
        {
            var dao = _context.Projects
                .SingleOrDefault(p => !p.IsArchived && p.ProjectId == projectId);

            if (dao == null)
                return null;

            return new ProjectDetails
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
