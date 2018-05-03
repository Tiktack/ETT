using ETT.Logic.DTO.Projects;
using ETT.Storage;
using ETT.Storage.Entities.Projects;
using ETT.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Logic.Managers.Projects
{
    public class ProjectManager : IDisposable
    {
        private UnitOfWork Database = new UnitOfWork();

        public int Create(ProjectDTO project, int userId)
        {
            var newProject = new Project()
            {
                Name = project.Name,
                Description = project.Description,
            };

            Database.Projects.Create(newProject);
            Database.Save();
            Database.ProjectUsers.Create(newProject.Id, userId, (int)ProjectRole.Owner);
            Database.Save();

            return newProject.Id;
        }

        public void Update(ProjectDTO project)
        {
            Database.Projects.Update(
                new Project()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    // tasks in taskService
                }
            );
            Database.Save();
        }

        public ProjectDTO Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            Project project = Database.Projects.Get(id);
            if (project == null) throw new KeyNotFoundException();

            return new ProjectDTO()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
            };
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            Database.Projects.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
