namespace Lifestyle.Planning.Infrastructure
{
    using System.Linq;
    using Domain;
    using Models;
    using Shared;

    public sealed class TaskRepository : ITaskRepository
    {
        private readonly LifestylePlanningDbContext _context;

        public TaskRepository(LifestylePlanningDbContext context)
        {
            Guard.ThrowIfNull(context, nameof(context));

            _context = context;
        }

        public Task FindById(TaskId id)
        {
            Guard.ThrowIfNull(id, nameof(id));

            var dao = _context.Tasks.SingleOrDefault(p => p.TaskId == id.Value);

            if (dao == null)
                return null;

            return new Task(dao.CreateState());
        }

        public TaskId GetNextIdentity()
        {
            return TaskId.New();
        }

        public void Save(Task aggregateRoot)
        {
            Guard.ThrowIfNull(aggregateRoot, nameof(aggregateRoot));

            var state = aggregateRoot.GetState();

            var existing = _context.Tasks.SingleOrDefault(p => p.TaskId == state.TaskId.Value);

            if (existing != null)
                existing.UpdateFrom(state);
            else
                _context.Tasks.Add(TaskDao.CreateFrom(state));

            _context.SaveChanges();
        }
    }
}
