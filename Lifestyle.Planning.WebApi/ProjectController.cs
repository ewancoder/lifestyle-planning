namespace Lifestyle.Planning.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Application;
    using Application.ReadModels;
    using Models;
    using Shared;

    [RoutePrefix("api/projects")]
    public sealed class ProjectController : ApiController
    {
        private readonly IProjectApplication _projectApp;
        private readonly IProjectQuery _projectQuery;

        public ProjectController(
            IProjectApplication projectApp,
            IProjectQuery projectQuery)
        {
            Guard.ThrowIfNull(projectApp, nameof(projectApp));
            Guard.ThrowIfNull(projectQuery, nameof(projectQuery));

            _projectApp = projectApp;
            _projectQuery = projectQuery;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ProjectInfo> FindAll()
        {
            return _projectQuery.FindAll();
        }

        [HttpGet]
        [Route("{projectId}")]
        public ProjectInfo FindById(Guid projectId)
        {
            return _projectQuery.FindById(projectId);
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
