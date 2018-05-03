using System;
using System.Linq;
using ETT.Logic.DTO.Projects;
using ETT.Logic.Interfaces;
using ETT.Logic.Services;
using ETT.Utils.Enums;
using ETT.Utils.Logger;
using ETT.Web.Models.Projects;
using ETT.Web.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETT.Web.Controllers.Projects
{
    public class ProjectController : Controller
    {
        private IProjectService service;

        // Dependency injection
        public ProjectController(IProjectService projectService)
        {
            this.service = projectService;
        }

        // GET: Project/Main/{id}
        public ActionResult Main(int id)
        {
            var project = service
                .AccessVerification(id, User.Identifier(), nameof(service.GetProject))
                .GetProject(id);

            return View(
                new ProjectViewModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                }
            );
        }

        // GET: Project/List
        public ActionResult List()
        {
            return View(
                new ListViewModel()
                {
                    Projects = service.GetProjects(User.Identifier()).Select(
                        project =>
                        new ProjectViewModel()
                        {
                            Id = project.Id,
                            Name = project.Name,
                            Description = project.Description,
                        }
                    )
                }
            );
        }

        // GET: Project/Details/{id}
        public ActionResult Details(int id)
        {
            var project = service
                .AccessVerification(id, User.Identifier(), nameof(service.GetProject))
                .GetProject(id);

            return View(
                new DetailsViewModel()
                {
                    Id = project.Id,
                    Admin = service.UserVerification(project.Id, User.Identifier(), ProjectRole.Admin),
                    Owner = service.UserVerification(project.Id, User.Identifier(), ProjectRole.Owner),
                    Name = project.Name,
                    Description = project.Description,
                }
            );
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View(/*new ProjectViewModel()*/);
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                int projectId = service.CreateProject(
                    new ProjectDTO()
                    {
                        Name = project.Name,
                        Description = project.Description,
                    },
                    User.Identifier()
                );

                return RedirectToAction(nameof(Main), new { id = projectId });
            }

            return View(project);
        }

        // GET: Project/Edit/{id}
        public ActionResult Edit(int id)
        {
            ProjectDTO project = service
                .AccessVerification(id, User.Identifier(), nameof(service.GetProject))
                .GetProject(id);

            return View(
                new ProjectViewModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                }
            );
        }

        // POST: Project/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                service
                    .AccessVerification(project.Id, User.Identifier(), nameof(service.UpdateProject))
                    .UpdateProject(
                    new ProjectDTO()
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Description = project.Description,
                    }
                );

                return RedirectToAction(nameof(Details), new { id = project.Id });
            }

            return View(project);
        }

        // GET: Project/Delete/{id}
        public ActionResult Delete(int id)
        {
            ProjectDTO project = service
                .AccessVerification(id, User.Identifier(), nameof(service.GetProject))
                .GetProject(id);

            return View(
                new ProjectViewModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                }
            );
        }

        // POST: Project/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            service
                .AccessVerification(id, User.Identifier(), nameof(service.DeleteProject))
                .DeleteProject(id);

            return View(new ProjectViewModel());
        }
    }
}