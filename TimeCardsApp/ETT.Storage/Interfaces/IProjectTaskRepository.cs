using ETT.Storage.Entities.Projects;
using System.Collections.Generic;

namespace ETT.Storage.Interfaces
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        IEnumerable<ProjectTask> GetAllByProject(int projectId);
    }
}
