using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETT.Logic.DTO.Projects;
using ETT.Logic.Interfaces;
using ETT.Utils.Enums;
using ETT.Web.Models.Projects.User;
using ETT.Web.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETT.Web.Controllers.Projects
{
    [Produces("application/json")]
    public class UserController : Controller
    {
        private IProjectService service;
        private IUserService userService;

        // Dependency injection
        public UserController(IProjectService projectService, IUserService userService)
        {
            this.service = projectService;
            this.userService = userService;
        }

        // GET: Project/{projectId}/User/Main
        public ActionResult Main(int projectId)
        {
            return View(
                new MainViewModel()
                {
                    ProjectId = projectId,
                    Admin = service.UserVerification(projectId, User.Identifier(), ProjectRole.Admin),
                }
            );
        }

        // GET: Project/{projectId}/User/Search
        [HttpGet]
        public IEnumerable<string> Search(int projectId, string email)
        {
            var projectUsers = service
                .AccessVerification(projectId, User.Identifier(), nameof(service.GetUsers))
                .GetUsers(projectId);

            return userService.SearchUsersByEmail(email?.Trim() ?? string.Empty, limit: 5)
                .Where(user => !projectUsers.Any(peojectUser => user.UserID == peojectUser.UserId))
                .Select(user => user.Email);
        }

        // POST: Project/{projectId}/User/Add
        [HttpPost]
        public void Add(int projectId, string email)
        {
            service
                .AccessVerification(projectId, User.Identifier(), nameof(service.AddUser))
                .AddUser(projectId, userService.GetUserByEmail(email?.Trim() ?? string.Empty)?.UserID ?? null, (int)ProjectRole.Visitor);
        }

        // PUT: Project/{projectId}/User/Update/{id}
        [HttpPut]
        public void Update(int projectId, int id, int roleId)
        {
            if (id == User.Identifier() && service.GetUserRole(projectId, User.Identifier()) == ProjectRole.Admin)
                throw new InvalidOperationException(nameof(Update));

            service
                .AccessVerification(projectId, User.Identifier(), nameof(service.UpdateUserRole))
                .UpdateUserRole(projectId, /*userId:*/ id, roleId);
        }

        // GET: Project/{projectId}/User/List
        [HttpGet]
        [Produces("text/html")]
        public ActionResult List(int projectId)
        {
            return View(new ListViewModel()
            {
                ProjectId = projectId,
                Admin = service.UserVerification(projectId, User.Identifier(), ProjectRole.Admin),
                Users = service
                    .AccessVerification(projectId, User.Identifier(), nameof(service.GetUsers))
                    .GetUsers(projectId).Select(projectUser =>
                    new UserViewModel()
                    {
                        UserId = projectUser.UserId,
                        Email = projectUser.Email,
                        Role = projectUser.Role,
                    }
                )
            });
        }

        // DELETE: Project/{projectId}/User/Delete/{id}
        [HttpDelete]
        public void Delete(int projectId, int id)
        {
            service
                .AccessVerification(projectId, User.Identifier(), nameof(service.DeleteUser))
                .DeleteUser(projectId, userId: id);
        }
    }
}
