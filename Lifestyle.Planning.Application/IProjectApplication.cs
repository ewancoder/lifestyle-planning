namespace Lifestyle.Planning.Application
{
    using System;

    public interface IProjectApplication
    {
        Guid CreateProject(string name);
        void RenameProject(Guid projectId, string newName);
        void ArchiveProject(Guid projectId);
    }
}
