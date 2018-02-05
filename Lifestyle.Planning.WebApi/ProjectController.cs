namespace Lifestyle.Planning.WebApi
{
    using System;
    using System.Web.Http;
    using Application;
    using Models;
    using Shared;

    [RoutePrefix("api/projects")]
    public sealed class ProjectController : ApiController
    {
        private readonly IProjectApplication _projectApp;

        public ProjectController(IProjectApplication projectApp)
        {
            Guard.ThrowIfNull(projectApp, nameof(projectApp));

            _projectApp = projectApp;
        }

        [HttpPost]
        [Route("")]
        public Guid Create(CreateProjectDto dto)
        {
            return _projectApp.CreateProject(dto.Name);
        }

        [HttpPut]
        [Route("{projectId}/rename")]
        public void Rename(Guid projectId, RenameProjectDto dto)
        {
            _projectApp.RenameProject(projectId, dto.Name);
        }

        [HttpDelete]
        [Route("{projectId}")]
        public void Archive(Guid projectId)
        {
            _projectApp.ArchiveProject(projectId);
        }
    }
}
