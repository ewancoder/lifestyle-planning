namespace Lifestyle.Planning.Application
{
    using System;
    using Domain;
    using Shared;

    public sealed class ProjectApplication : IProjectApplication
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectApplication(IProjectRepository projectRepository)
        {
            Guard.ThrowIfNull(projectRepository, nameof(projectRepository));

            _projectRepository = projectRepository;
        }

        public void ArchiveProject(Guid projectId)
        {
            var aProjectId = new ProjectId(projectId);

            var project = _projectRepository.FindById(aProjectId);
            if (project == null)
                throw new InvalidOperationException($"Project {projectId} does not exist.");

            project.Archive();
            _projectRepository.Save(project);
        }

        public Guid CreateProject(string name)
        {
            var aProjectName = new ProjectName(name);
            var aProjectId = _projectRepository.GetNextIdentity();

            var project = new Project(aProjectId, aProjectName);
            _projectRepository.Save(project);

            return aProjectId.Value;
        }

        public void RenameProject(Guid projectId, string newName)
        {
            var aProjectId = new ProjectId(projectId);
            var newProjectName = new ProjectName(newName);

            var project = _projectRepository.FindById(aProjectId);
            if (project == null)
                throw new InvalidOperationException($"Project {projectId} does not exist.");

            project.Rename(newProjectName);
            _projectRepository.Save(project);
        }
    }
}
