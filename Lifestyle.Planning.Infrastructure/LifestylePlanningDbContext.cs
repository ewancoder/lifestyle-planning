namespace Lifestyle.Planning.Infrastructure
{
    using System.Data.Entity;
    using Models;

    public sealed class LifestylePlanningDbContext : DbContext
    {
        public DbSet<ProjectDao> Projects { get; set; }
        public DbSet<TaskDao> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.SetupDefaults();
        }
    }
}
