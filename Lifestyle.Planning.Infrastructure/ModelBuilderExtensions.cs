namespace Lifestyle.Planning.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Text.RegularExpressions;

    public static class ModelBuilderExtensions
    {
        public static void SetupDefaults(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder
                .Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

            modelBuilder
                .Types()
                .Configure(c => c.ToTable(GetTableName(c.ClrType.Name)));

            modelBuilder
                .Properties()
                .Configure(p => p.HasColumnName(GetTableName(p.ClrPropertyInfo.Name)));

            modelBuilder
                .Properties<decimal>()
                .Configure(c => c.HasPrecision(10, 4));

            modelBuilder
                .Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2").HasPrecision(3));
        }

        /// <summary>
        /// Changes "TableName" to "table_name".
        /// </summary>
        /// <param name="name">Name that needs to be changed.</param>
        /// <returns>Name of the table/column.</returns>
        private static string GetTableName(string name)
        {
            return Regex.Replace(name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]).ToLowerInvariant();
        }
    }
}
