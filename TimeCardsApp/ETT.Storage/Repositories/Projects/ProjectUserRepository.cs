using ETT.Storage.Context;
using ETT.Storage.Entities.Projects;
using ETT.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Storage.Repositories.Projects
{
    public class ProjectUserRepository : IProjectUserRepository
    {
        private ApplicationContext context;

        public ProjectUserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(int projectId, int userId, int roleId)
        {
            context.ProjectUsers.Add(
                new ProjectUser()
                {
                    ProjectId = projectId,
                    UserId = userId,
                    RoleId = roleId,
                }
            );
        }
        
        public void Delete(int projectId, int userId)
        {
            var projectUser = Get(projectId, userId);

            if (projectUser != null)
                context.ProjectUsers.Remove(projectUser);
        }
        
        public ProjectUser Get(int projectId, int userId)
        {
            return context.ProjectUsers.Where(user => user.ProjectId == projectId).FirstOrDefault(user => user.UserId == userId);
        }

        public IEnumerable<ProjectUser> GetAllWithFilter(Func<ProjectUser, bool> where)
        {
            return context.ProjectUsers.Where(where);
        }
        public IEnumerable<ProjectUser> GetAllWithFilter(Func<ProjectUser, bool> where, Func<ProjectUser, object> orderBy)
        {
            return context.ProjectUsers.Where(where).OrderBy(orderBy);
        }
        public IEnumerable<ProjectUser> GetAllWithFilter(Func<ProjectUser, bool> where, Func<ProjectUser, object> orderBy, int skip, int limit)
        {
            return context.ProjectUsers.Where(where).OrderBy(orderBy).Skip(skip).Take(limit);
        }

        public void Update(ProjectUser item)
        {
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
