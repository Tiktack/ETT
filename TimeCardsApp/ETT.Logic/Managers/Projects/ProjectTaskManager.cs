using ETT.Logic.DTO.Projects;
using ETT.Storage;
using ETT.Storage.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Logic.Managers.Projects
{
    public class ProjectTaskManager : IDisposable
    {
        private UnitOfWork Database = new UnitOfWork();

        public int Create(ProjectTaskDTO task)
        {
            var newTask = new ProjectTask()
            {
                Name = task.Name,
                Description = task.Description,
                CreationDate = DateTime.UtcNow,
                ProjectId = task.ProjectId,
            };

            Database.ProjectTasks.Create(newTask);
            Database.Save();

            return newTask.Id;
        }

        public void Update(ProjectTaskDTO task)
        {
            Database.ProjectTasks.Update(
                new ProjectTask()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    CreationDate = task.CreationDate,
                    CompletionDate = task.CompletionDate,
                    ProjectId = task.ProjectId, // ?
                }
            );
            Database.Save();
        }

        public ProjectTaskDTO Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            ProjectTask task = Database.ProjectTasks.Get(id);
            if (task == null) throw new KeyNotFoundException();

            return new ProjectTaskDTO()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreationDate = task.CreationDate,
                CompletionDate = task.CompletionDate,
                ProjectId = task.ProjectId,
            };
        }

        public IEnumerable<ProjectTaskDTO> GetAll(int projectId)
        {
            if (projectId <= 0) throw new ArgumentOutOfRangeException(nameof(projectId));

            IEnumerable<ProjectTaskDTO> tasks = Database.ProjectTasks.GetAllByProject(projectId).Select(
                task =>
                new ProjectTaskDTO()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    CreationDate = task.CreationDate,
                    CompletionDate = task.CompletionDate,
                    ProjectId = task.ProjectId,
                }
            );

            return tasks;
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            Database.ProjectTasks.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
