using ETT.Storage.Entities.Projects;
using System;
using System.Collections.Generic;

namespace ETT.Storage.Interfaces
{
    public interface IProjectUserRepository
    {
        void Create(int projectId, int userId, int roleId);
        void Delete(int projectId, int userId);
        ProjectUser Get(int projectId, int userId);
        void Update(ProjectUser item);

        IEnumerable<ProjectUser> GetAllWithFilter(Func<ProjectUser, bool> where);
        IEnumerable<ProjectUser> GetAllWithFilter(Func<ProjectUser, bool> where, Func<ProjectUser, object> orderBy);
        IEnumerable<ProjectUser> GetAllWithFilter(Func<ProjectUser, bool> where, Func<ProjectUser, object> orderBy, int skip, int limit);
    }
}
