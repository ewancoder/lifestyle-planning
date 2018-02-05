namespace Lifestyle.Planning.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain;

    [Table("task")]
    public class TaskDao
    {
        [Key]
        public int RowId { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectRowId { get; set; }
        public ProjectDao Project { get; set; }

        [Index("ix_task_id", IsUnique = true)]
        public Guid TaskId { get; set; }

        [Index("ix_project_id")]
        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public static TaskDao CreateFrom(Task.State state)
        {
            var dao = new TaskDao();

            dao.UpdateFrom(state);

            return dao;
        }

        public void UpdateFrom(Task.State state)
        {
            TaskId = state.TaskId.Value;
            ProjectId = state.ProjectId.Value;
            Name = state.Name.Value;
            IsArchived = state.IsArchived;
        }

        public Task.State CreateState()
        {
            return new Task.State
            {
                TaskId = new TaskId(TaskId),
                ProjectId = new ProjectId(ProjectId),
                Name = new TaskName(Name),
                IsArchived = IsArchived
            };
        }
    }
}
