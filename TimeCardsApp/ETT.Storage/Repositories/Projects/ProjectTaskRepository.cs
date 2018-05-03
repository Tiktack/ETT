using ETT.Storage.Context;
using ETT.Storage.Entities.Projects;
using ETT.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Storage.Repositories.Projects
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private ApplicationContext context;

        public ProjectTaskRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(ProjectTask item)
        {
            context.Tasks.Add(item);
        }

        public void Delete(int id)
        {
            ProjectTask task = context.Tasks.Find(id);
            if (task != null)
                context.Tasks.Remove(task);
        }

        public ProjectTask Get(int id)
        {
            return context.Tasks.Find(id);
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectTask> GetAllByProject(int projectId)
        {
            return context.Tasks.Where(task => task.ProjectId == projectId);
        }

        public void Update(ProjectTask item)
        {
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
