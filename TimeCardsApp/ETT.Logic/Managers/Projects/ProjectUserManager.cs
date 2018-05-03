using ETT.Logic.DTO.Projects;
using ETT.Storage;
using ETT.Storage.Entities.Projects;
using ETT.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Logic.Managers.Projects
{
    public class ProjectUserManager : IDisposable
    {
        private UnitOfWork Database = new UnitOfWork();

        public void Add(int projectId, int userId, int roleId)
        {
            if (Database.ProjectUsers.Get(projectId, userId) == default(ProjectUser))
            {
                Database.ProjectUsers.Create(projectId, userId, roleId);
                Database.Save();
            }
        }

        public void Update(int projectId, int userId, int roleId)
        {
            var projectUser = Database.ProjectUsers.Get(projectId, userId);

            if (roleId == (int)ProjectRole.Owner || projectUser.RoleId == (int)ProjectRole.Owner)
                throw new ArgumentException(nameof(roleId));

            projectUser.RoleId = roleId;

            Database.ProjectUsers.Update(projectUser);
            Database.Save();
        }

        public ProjectRole GetRole(int projectId, int userId)
        {
            return (ProjectRole)Database.ProjectUsers.Get(projectId, userId).RoleId;
        }

        public IEnumerable<ProjectUserDTO> GetAll(int projectId)
        {
            return Database.ProjectUsers.GetAllWithFilter(user => user.ProjectId == projectId).Select(
                projectUser =>
                new ProjectUserDTO()
                {
                    UserId = projectUser.UserId,
                    Email = Database.Users.Get(projectUser.UserId).Email,
                    Role = (ProjectRole)projectUser.RoleId,
                }
            );
        }

        public IEnumerable<ProjectDTO> GetAllProjects(int userId, int roleId)
        {
            return Database.ProjectUsers
                .GetAllWithFilter(projectUser => projectUser.UserId == userId && projectUser.RoleId <= roleId)
                .Select(user => Database.Projects.Get(user.ProjectId))
                .Select(project =>
                new ProjectDTO()
                {
                    Id = project.Id,
                    Name = project.Name,
                    //Description = project.Description,
                }
            );
        }

        public void Delete(int projectId, int userId)
        {
            if (GetRole(projectId, userId) == ProjectRole.Owner) throw new ArgumentException(nameof(userId));

            Database.ProjectUsers.Delete(projectId, userId);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
