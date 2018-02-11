namespace Lifestyle.Planning.WebApi.Models
{
    using System;

    public sealed class CreateTaskDto
    {
        public Guid ProjectId { get; set; }
        public int StageId { get; set; }
        public string Name { get; set; }
    }
}
