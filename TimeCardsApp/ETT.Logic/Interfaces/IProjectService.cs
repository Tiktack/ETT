using ETT.Logic.DTO.Projects;
using ETT.Logic.Services;
using ETT.Utils.Enums;
using System;
using System.Collections.Generic;

namespace ETT.Logic.Interfaces
{
    public interface IProjectService : IDisposable
    {
        int CreateProject(ProjectDTO project, int? userId);
        void UpdateProject(ProjectDTO project);
        ProjectDTO GetProject(int? id);
        IEnumerable<ProjectDTO> GetProjects(int? userId);
        void DeleteProject(int? id);
       

        int CreateTask(ProjectTaskDTO task);
        void UpdateTask(ProjectTaskDTO task);
        ProjectTaskDTO GetTask(int? id);
        IEnumerable<ProjectTaskDTO> GetTasks(int? projectId);
        void DeleteTask(int? id);

        void AddUser(int? projectId, int? userId, int? roleId);
        void UpdateUserRole(int? projectId, int? userId, int? roleId);
        ProjectRole GetUserRole(int? projectId, int? userId);
        IEnumerable<ProjectDTO> GetUserProjects(int? userId, int? roleId);
        IEnumerable<ProjectUserDTO> GetUsers(int? projectId);
        void DeleteUser(int? projectId, int? userId);

        ProjectService AccessVerification(int? projectId, int? userId, string action);
        bool UserVerification(int? projectId, int? userId, ProjectRole role);
    }
}
