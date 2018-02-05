namespace Lifestyle.Planning.Autofac
{
    using global::Autofac;
    using Lifestyle.Planning.Application;
    using Lifestyle.Planning.Domain;
    using Lifestyle.Planning.Infrastructure;

    public sealed class LifestylePlanningModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LifestylePlanningDbContext>().AsSelf()
                .InstancePerLifetimeScope(); // TODO: Check that it works as intended.

            RegisterProjectModule(builder);
            RegisterTaskModule(builder);
        }

        private static void RegisterProjectModule(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectRepository>()
                .As<IProjectRepository>();

            builder.RegisterType<ProjectApplication>()
                .As<IProjectApplication>();
        }

        private static void RegisterTaskModule(ContainerBuilder builder)
        {
            builder.RegisterType<TaskRepository>()
                .As<ITaskRepository>();

            builder.RegisterType<TaskApplication>()
                .As<ITaskApplication>();
        }
    }
}
