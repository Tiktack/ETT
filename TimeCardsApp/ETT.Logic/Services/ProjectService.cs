using System;
using System.Collections.Generic;
using ETT.Logic.Interfaces;
using ETT.Logic.DTO.Projects;
using ETT.Logic.Managers.Projects;
using ETT.Utils.Enums;

namespace ETT.Logic.Services
{
    public class ProjectService : IProjectService
    {
        private ProjectManager projectManager = null;
        private ProjectManager Project => projectManager ?? (projectManager = new ProjectManager());

        private ProjectTaskManager taskManager = null;
        private ProjectTaskManager Task => taskManager ?? (taskManager = new ProjectTaskManager());

        private ProjectUserManager userManager = null;
        private ProjectUserManager User => userManager ?? (userManager = new ProjectUserManager());

        private ProjectRoleManager roleManager = null;
        private ProjectRoleManager Role => roleManager ?? (roleManager = new ProjectRoleManager(this));

        // PROJECTS

        [ProjectPermissions]
        public int CreateProject(ProjectDTO project, int? userId)
        {
            return Project.Create(
                project ?? throw new ArgumentNullException(nameof(project)),
                userId ?? throw new ArgumentNullException(nameof(userId))
            );
        }

        [ProjectPermissions(ProjectRole.Admin)]
        public void UpdateProject(ProjectDTO project)
        {
            Project.Update(project ?? throw new ArgumentNullException(nameof(project)));
        }

        [ProjectPermissions]
        public ProjectDTO GetProject(int? id)
        {
            return Project.Get(id ?? throw new ArgumentNullException(nameof(id)));
        }

        [ProjectPermissions]
        public IEnumerable<ProjectDTO> GetProjects(int? userId)
        {
            return GetUserProjects(userId, (int)ProjectRole.Visitor);
        }

        [ProjectPermissions(ProjectRole.Owner)]
        public void DeleteProject(int? id)
        {
            Project.Delete(id ?? throw new ArgumentNullException(nameof(id)));
        }

        // TASKS

        [ProjectPermissions(ProjectRole.Admin, ProjectRole.Manager)]
        public int CreateTask(ProjectTaskDTO task)
        {
            return Task.Create(task ?? throw new ArgumentNullException(nameof(task)));
        }

        [ProjectPermissions(ProjectRole.Admin, ProjectRole.Manager)]
        public void UpdateTask(ProjectTaskDTO task)
        {
            Task.Update(task ?? throw new ArgumentNullException(nameof(task)));
        }

        [ProjectPermissions]
        public ProjectTaskDTO GetTask(int? id)
        {
            return Task.Get(id ?? throw new ArgumentNullException(nameof(id)));
        }

        [ProjectPermissions]
        public IEnumerable<ProjectTaskDTO> GetTasks(int? projectId)
        {
            return Task.GetAll(projectId ?? throw new ArgumentNullException(nameof(projectId)));
        }

        [ProjectPermissions(ProjectRole.Admin, ProjectRole.Manager)]
        public void DeleteTask(int? id)
        {
            Task.Delete(id ?? throw new ArgumentNullException(nameof(id)));
        }

        // USERS

        [ProjectPermissions(ProjectRole.Admin)]
        public void AddUser(int? projectId, int? userId, int? roleId)
        {
            User.Add(
                projectId ?? throw new ArgumentNullException(nameof(projectId)),
                userId ?? throw new ArgumentNullException(nameof(userId)),
                roleId ?? throw new ArgumentNullException(nameof(roleId))
            );
        }

        [ProjectPermissions(ProjectRole.Admin)]
        public void UpdateUserRole(int? projectId, int? userId, int? roleId)
        {
            User.Update(
                projectId ?? throw new ArgumentNullException(nameof(projectId)),
                userId ?? throw new ArgumentNullException(nameof(userId)),
                roleId ?? throw new ArgumentNullException(nameof(roleId))
            );
        }

        [ProjectPermissions]
        public ProjectRole GetUserRole(int? projectId, int? userId)
        {
            return User.GetRole(
                projectId ?? throw new ArgumentNullException(nameof(projectId)),
                userId ?? throw new ArgumentNullException(nameof(userId))
            );
        }

        [ProjectPermissions]
        public IEnumerable<ProjectDTO> GetUserProjects(int? userId, int? roleId)
        {
            return User.GetAllProjects(
                userId ?? throw new ArgumentNullException(nameof(userId)),
                roleId ?? throw new ArgumentNullException(nameof(roleId))
            );
        }

        [ProjectPermissions]
        public IEnumerable<ProjectUserDTO> GetUsers(int? projectId)
        {
            return User.GetAll(projectId ?? throw new ArgumentNullException(nameof(projectId)));
        }

        [ProjectPermissions(ProjectRole.Admin)]
        public void DeleteUser(int? projectId, int? userId)
        {
            User.Delete(
                projectId ?? throw new ArgumentNullException(nameof(projectId)),
                userId ?? throw new ArgumentNullException(nameof(userId))
            );
        }
        
        // ROLES

        public ProjectService AccessVerification(int? projectId, int? userId, string action)
        {
            Role.Verify(
                projectId ?? throw new ArgumentNullException(nameof(projectId)),
                userId ?? throw new ArgumentNullException(nameof(userId)),
                action ?? throw new ArgumentNullException(nameof(action))
            );

            return this;
        }

        public bool UserVerification(int? projectId, int? userId, ProjectRole role)
        {
            ProjectRole[] roles;

            switch(role)
            {
                case ProjectRole.Owner:
                    roles = new[] { ProjectRole.Owner };
                    break;
                case ProjectRole.Admin:
                    roles = new[] { ProjectRole.Admin, ProjectRole.Owner };
                    break;
                case ProjectRole.Manager:
                    roles = new[] { ProjectRole.Manager, ProjectRole.Admin, ProjectRole.Owner };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(role));
            }

            return Role.Verify(
                projectId ?? throw new ArgumentNullException(nameof(projectId)),
                userId ?? throw new ArgumentNullException(nameof(userId)),
                roles
            );
        }

        public void Dispose()
        {
            projectManager?.Dispose();
            taskManager?.Dispose();
            userManager?.Dispose();
            roleManager?.Dispose();
        }
    }
}
