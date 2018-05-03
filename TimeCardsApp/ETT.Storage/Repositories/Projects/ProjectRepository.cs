using ETT.Storage.Context;
using ETT.Storage.Entities.Projects;
using ETT.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Storage.Repositories.Projects
{
    public class ProjectRepository : IProjectRepository
    {
        private ApplicationContext context;

        public ProjectRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Project item)
        {
            context.Projects.Add(item);
        }

        public void Delete(int id)
        {
            Project project = context.Projects.Find(id);
            if (project != null)
                context.Projects.Remove(project);
        }

        public Project Get(int id)
        {
            return context.Projects.Find(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return context.Projects;
        }
        public IEnumerable<Project> GetAllWithFilter(Func<Project, bool> where)
        {
            return context.Projects.Where(where);
        }
        public IEnumerable<Project> GetAllWithFilter(Func<Project, bool> where, Func<Project, object> orderBy)
        {
            return context.Projects.Where(where).OrderBy(orderBy);
        }
        public IEnumerable<Project> GetAllWithFilter(Func<Project, bool> where, Func<Project, object> orderBy, int skip = 0, int limit = 10)
        {
            return context.Projects.Where(where).OrderBy(orderBy).Skip(skip).Take(limit);
        }

        public IEnumerable<Project> GetAllWithLimit(int limit)
        {
            return context.Projects.OrderByDescending(p => p.Id).Take(limit);
        }

        public void Update(Project item)
        {
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
