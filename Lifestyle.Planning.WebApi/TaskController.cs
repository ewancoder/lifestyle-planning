namespace Lifestyle.Planning.WebApi
{
    using System;
    using System.Web.Http;
    using Application;
    using Models;
    using Shared;

    [RoutePrefix("api/tasks")]
    public sealed class TaskController : ApiController
    {
        private readonly ITaskApplication _taskApp;

        public TaskController(ITaskApplication taskApp)
        {
            Guard.ThrowIfNull(taskApp, nameof(taskApp));

            _taskApp = taskApp;
        }

        [HttpPost]
        [Route("")]
        public Guid Create(CreateTaskDto dto)
        {
            return _taskApp.CreateTask(dto.ProjectId, dto.StageId, dto.Name);
        }

        [HttpPut]
        [Route("{taskId}/rename")]
        public void Rename(Guid taskId, RenameTaskDto dto)
        {
            _taskApp.RenameTask(taskId, dto.Name);
        }

        [HttpDelete]
        [Route("{taskId}")]
        public void Archive(Guid taskId)
        {
            _taskApp.ArchiveTask(taskId);
        }
    }
}
