namespace Lifestyle.Planning.Application
{
    using System;
    using Domain;
    using Shared;

    public sealed class TaskApplication : ITaskApplication
    {
        private readonly ITaskRepository _taskRepository;

        public TaskApplication(ITaskRepository taskRepository)
        {
            Guard.ThrowIfNull(taskRepository, nameof(taskRepository));

            _taskRepository = taskRepository;
        }

        public void ArchiveTask(Guid taskId)
        {
            var aTaskId = new TaskId(taskId);

            var task = _taskRepository.FindById(aTaskId);
            if (task == null)
                throw new InvalidOperationException($"Task {taskId} does not exist.");

            task.Archive();
            _taskRepository.Save(task);
        }

        public Guid CreateTask(Guid projectId, string name)
        {
            var aTaskName = new TaskName(name);
            var aProjectId = new ProjectId(projectId);
            var aTaskId = _taskRepository.GetNextIdentity();

            var task = new Task(aTaskId, aProjectId, aTaskName);
            _taskRepository.Save(task);

            return aTaskId.Value;
        }

        public void RenameTask(Guid taskId, string newName)
        {
            var aTaskId = new TaskId(taskId);
            var newTaskName = new TaskName(newName);

            var task = _taskRepository.FindById(aTaskId);
            if (task == null)
                throw new InvalidOperationException($"Task {taskId} does not exist.");

            task.Rename(newTaskName);
            _taskRepository.Save(task);
        }
    }
}
