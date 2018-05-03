using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETT.Logic.DTO.Projects;
using ETT.Logic.Interfaces;
using ETT.Utils.Enums;
using ETT.Web.Models.Projects.Task;
using ETT.Web.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETT.Web.Controllers.Projects
{
    public class TaskController : Controller
    {
        private IProjectService service;

        // Dependency injection
        public TaskController(IProjectService projectService)
        {
            this.service = projectService;
        }

        // GET: Project/{projectId}/Task/Main
        public ActionResult Main(int projectId)
        {
            return View(
                new MainViewModel()
                {
                    projectId = projectId,
                    Manager = service.UserVerification(projectId, User.Identifier(), ProjectRole.Manager),
                }
            );
        }

        // GET: Project/{projectId}/Task/List/
        public ActionResult List(int projectId)
        {
            return View(
                new ListViewModel()
                {
                    projectId = projectId,
                    Manager = service.UserVerification(projectId, User.Identifier(), ProjectRole.Manager),
                    Tasks = service
                        .AccessVerification(projectId, User.Identifier(), nameof(service.GetTasks))
                        .GetTasks(projectId).Select(task =>
                        new TaskViewModel()
                        {
                            Id = task.Id,
                            Name = task.Name,
                            //Description = task.Description,
                            CreationDate = task.CreationDate,
                        }
                    ),
                }
            );
        }

        // GET: Project/{projectId}/Task/Create/
        public ActionResult Create(int projectId)
        {
            return View(
                new TaskViewModel()
                {
                    ProjectId = projectId,
                }
            );
        }

        // POST: Project/{projectId}/Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                task.Id = service
                    .AccessVerification(projectId, User.Identifier(), nameof(service.CreateTask))
                    .CreateTask(
                    new ProjectTaskDTO()
                    {
                        Name = task.Name,
                        Description = task.Description,
                        CreationDate = DateTime.UtcNow,
                        ProjectId = projectId,
                    }
                );

                return RedirectToAction(nameof(Details), new { projectId = task.ProjectId, id = task.Id });
            }

            return View(task);
        }

        // GET: Project/{projectId}/Task/Edit/{id}
        public ActionResult Edit(int projectId, int id)
        {
            ProjectTaskDTO task = service
                .AccessVerification(projectId, User.Identifier(), nameof(service.GetTask))
                .GetTask(id);

            return View(
                new TaskViewModel()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    CreationDate = task.CreationDate,
                    ProjectId = task.ProjectId,
                }
            );
        }

        // POST: Project/{projectId}/Task/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int projectId, int id, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                service
                    .AccessVerification(projectId, User.Identifier(), nameof(service.UpdateTask))
                    .UpdateTask(
                    new ProjectTaskDTO()
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        CreationDate = task.CreationDate,
                        ProjectId = task.ProjectId,
                    }
                );

                return RedirectToAction(nameof(Details), new { projectId = task.ProjectId, id = task.Id });
            }

            return View(task);
        }

        // GET: Project/{projectId}/Task/Details/{id}
        public ActionResult Details(int projectId, int id)
        {
            ProjectTaskDTO task = service
                .AccessVerification(projectId, User.Identifier(), nameof(service.GetTask))
                .GetTask(id);

            return View(
                new TaskViewModel()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    CreationDate = task.CreationDate,
                    ProjectId = task.ProjectId,
                }
            );
        }

        // GET: Project/{projectId}/Task/Delete/{id}
        public ActionResult Delete(int projectId, int id)
        {
            ProjectTaskDTO task = service
                .AccessVerification(projectId, User.Identifier(), nameof(service.GetTask))
                .GetTask(id);

            return View(
                new TaskViewModel()
                {
                    Id = task.Id,
                    Name = task.Name,
                    //Description = task.Description,
                    CreationDate = task.CreationDate,
                    ProjectId = task.ProjectId,
                }
            );
        }

        // POST: Project/{projectId}/Task/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int projectId, int id, IFormCollection collection)
        {
            service
                .AccessVerification(projectId, User.Identifier(), nameof(service.DeleteTask))
                .DeleteTask(id);

            return RedirectToAction(nameof(Main), new { projectId });
        }
    }
}