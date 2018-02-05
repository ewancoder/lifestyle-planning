namespace Lifestyle.Planning.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain;

    [Table("project")]
    public class ProjectDao
    {
        [Key]
        [Column("row_id")]
        public int RowId { get; set; }

        [Column("project_id")]
        [Index("ix_project_id", IsUnique = true)]
        public Guid ProjectId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_archived")]
        public bool IsArchived { get; set; }

        public static ProjectDao CreateFrom(Project.State state)
        {
            var dao = new ProjectDao();

            dao.UpdateFrom(state);

            return dao;
        }

        public void UpdateFrom(Project.State state)
        {
            ProjectId = state.ProjectId.Value;
            Name = state.Name.Value;
            IsArchived = state.IsArchived;
        }

        public Project.State CreateState()
        {
            return new Project.State
            {
                ProjectId = new ProjectId(ProjectId),
                Name = new ProjectName(Name),
                IsArchived = IsArchived
            };
        }
    }
}
