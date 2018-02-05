namespace Lifestyle.Planning.Application
{
    using System;

    public interface ITaskApplication
    {
        Guid CreateTask(Guid projectId, string name);
        void RenameTask(Guid taskId, string newName);
        void ArchiveTask(Guid taskId);
    }
}
