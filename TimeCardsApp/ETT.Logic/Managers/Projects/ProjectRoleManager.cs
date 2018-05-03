using ETT.Storage;
using ETT.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Logic.Managers.Projects
{
    public class ProjectPermissionsAttribute : Attribute
    {
        public IEnumerable<ProjectRole> Roles { get; private set; }

        public ProjectPermissionsAttribute(params ProjectRole[] roles)
        {
            if (roles.Length > 0)
            {
                Roles = roles.Contains(ProjectRole.Owner) ? roles : roles.Concat(new[] { ProjectRole.Owner });
            }
            else
            {
                Roles = Enum.GetValues(typeof(ProjectRole)).Cast<ProjectRole>();
            }
        }
    }

    public class ProjectRoleManager : IDisposable
    {
        private UnitOfWork Database = new UnitOfWork();
        private Dictionary<string, ProjectRole[]> Actions;

        public ProjectRoleManager(object @class)
        {
            Actions = (@class?.GetType() ?? throw new ArgumentNullException(nameof(@class)))
                .GetMethods()
                .Where(method => method.GetCustomAttributes(typeof(ProjectPermissionsAttribute), false).Length > 0)
                .ToDictionary(
                    method => method.Name,
                    method => ((ProjectPermissionsAttribute)method.GetCustomAttributes(typeof(ProjectPermissionsAttribute), false)[0]).Roles.ToArray()
                );
        }

        public void Verify(int projectId, int userId, string action)
        {
            if (Verify(projectId, userId, Actions[action]) == false)
            {
                throw new MethodAccessException(action);
            }
        }

        public bool Verify(int projectId, int userId, params ProjectRole[] roles)
        {
            return roles.Contains(
                (ProjectRole)Database.ProjectUsers.Get(projectId, userId).RoleId
            );
        }

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //public string CurrentActionName()
        //{
        //    return new StackTrace().GetFrame(1).GetMethod().Name;
        //}

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
