﻿namespace Lifestyle.Planning.Application.ReadModels
{
    using System;
    using System.Collections.Generic;

    public sealed class ProjectDetails
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskInfo> Tasks { get; set; }
    }
}
