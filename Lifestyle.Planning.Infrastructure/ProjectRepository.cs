namespace Lifestyle.Planning.Infrastructure
{
    using System.Linq;
    using Domain;
    using Models;
    using Shared;

    public sealed class ProjectRepository : IProjectRepository
    {
        private readonly LifestylePlanningDbContext _context;

        public ProjectRepository(LifestylePlanningDbContext context)
        {
            Guard.ThrowIfNull(context, nameof(context));

            _context = context;
        }

        public Project FindById(ProjectId id)
        {
            Guard.ThrowIfNull(id, nameof(id));

            var dao = _context.Projects.SingleOrDefault(p => p.ProjectId == id.Value);

            if (dao == null)
                return null;

            return new Project(dao.CreateState());
        }

        public ProjectId GetNextIdentity()
        {
            return ProjectId.New();
        }

        public void Save(Project aggregateRoot)
        {
            Guard.ThrowIfNull(aggregateRoot, nameof(aggregateRoot));

            var state = aggregateRoot.GetState();

            var existing = _context.Projects.SingleOrDefault(p => p.ProjectId == state.ProjectId.Value);

            if (existing != null)
                existing.UpdateFrom(state);
            else
                _context.Projects.Add(ProjectDao.CreateFrom(state));

            _context.SaveChanges();
        }
    }
}
