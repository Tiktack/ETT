using ETT.Storage.Entities.Projects;
using System;
using System.Collections.Generic;

namespace ETT.Storage.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetAllWithFilter(Func<Project, bool> where);
        IEnumerable<Project> GetAllWithFilter(Func<Project, bool> where, Func<Project, object> orderBy);
        IEnumerable<Project> GetAllWithFilter(Func<Project, bool> where, Func<Project, object> orderBy, int skip = 0, int limit = 10);

        IEnumerable<Project> GetAllWithLimit(int limit);
    }
}
