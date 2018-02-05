namespace Lifestyle.Planning.Application.ReadModels
{
    using System;
    using System.Collections.Generic;

    public interface IProjectQuery
    {
        IEnumerable<ProjectInfo> FindAll();
        ProjectInfo FindById(Guid projectId);
    }
}
