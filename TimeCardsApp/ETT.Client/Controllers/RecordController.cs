using System;
using System.Collections.Generic;
using System.Linq;
using ETT.Logic.DTO;
using ETT.Logic.Interfaces;
using ETT.Logic.Services;
using ETT.Utils.Logger;
using ETT.Web.Models.Record;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ETT.Web.Util.Reporting;
using ETT.Logic.DTO.Projects;
using Microsoft.Extensions.Localization;
using ETT.Utils.Enums;
using System.Globalization;

namespace ETT.Web.Controllers
{
    public class RecordController : Controller
    {
        private IRecordService service;
        private IUserService users;
        private IProjectService projects;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        // Dependency injection
        public RecordController(IRecordService recordService, IUserService userService, IProjectService projectService,
                   IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.service = recordService;
            this.users = userService;
            this.projects = projectService;
            _sharedLocalizer = sharedLocalizer;
        }


        public ActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public ActionResult PdfWriter(IEnumerable<RecordViewModel> records)
        {
            ToPDF pdfwriter = new ToPDF();
            pdfwriter.RecordsToPDF(records);
            return PartialView();
        }

        // GET: Record/List

        [Authorize(Roles = "admin")]
        public ActionResult List()
        {
            return View(
                new RecordListViewModel()
                {
                    Records = service.GetRecords().Select(
                        record =>
                        new RecordViewModel()
                        {
                            Id = record.Id,
                            UserId = record.UserId,
                            TaskId = record.TaskId,
                            Hours = record.Hours,
                            Note = record.Note,
                            RecordDateTime = record.RecordDateTime,
                            ProjectId = record.ProjectId
                        }
                    )
                }
            );
        }
        // sort Action
        public RecordListViewModel SortBy(SortState sortParam, IEnumerable<RecordViewModel> records)
        {
            RecordListViewModel model = new RecordListViewModel();
            switch (sortParam)
            {
                case SortState.IdAsc:
                    {
                        model.Records = records.OrderBy(s => s.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.Id);
                        break;
                    }
                case SortState.UserIdAsc:
                    {
                        model.Records = records.OrderBy(s => s.UserId);
                        break;
                    }
                case SortState.UserIdDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.UserId);
                        break;
                    }
                case SortState.ProjectNameAsc:
                    {
                        model.Records = records.OrderBy(s => s.ProjectName);
                        break;
                    }
                case SortState.ProjectNameDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.ProjectName);
                        break;
                    }
                case SortState.TaskNameAsc:
                    {
                        model.Records = records.OrderBy(s => s.TaskName);
                        break;
                    }
                case SortState.TaskNameDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.TaskName);
                        break;
                    }
                case SortState.NoteAsc:
                    {
                        model.Records = records.OrderBy(s => s.Note);
                        break;
                    }
                case SortState.NoteDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.Note);
                        break;
                    }
                case SortState.HourAsc:
                    {
                        model.Records = records.OrderBy(s => s.Hours);
                        break;
                    }
                case SortState.HourDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.Hours);
                        break;
                    }
                case SortState.DataTimeAsc:
                    {
                        model.Records = records.OrderBy(s => s.RecordDateTime);
                        break;
                    }
                case SortState.DataTimeDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.RecordDateTime);
                        break;
                    }
                default:
                    {
                        model.Records = records.OrderBy(s => s.Id);
                        break;
                    }
            }
            return model;
        }
        // GET: Record/UserList
        public ActionResult UserList(SortState sort = SortState.IdAsc, int lastsort = 0)
        {
            int userId = users.GetUserIdByName(User.Identity.Name);
            var records = service.GetRecords().Where(record => record.UserId == userId).Select(
                  record =>
                    new RecordViewModel()
                    {
                        Id = record.Id,
                        UserId = userId,
                        ProjectId = record.ProjectId,
                        TaskId = record.TaskId,
                        TaskName = projects.GetTask(record.TaskId).Name,
                        ProjectName = projects.GetProject(record.ProjectId).Name,
                        Hours = record.Hours,
                        Note = record.Note,
                        RecordDateTime = record.RecordDateTime,
                    }
                );
            ViewData["IdSort"]          = sort == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["UserIdSort"]      = sort == SortState.UserIdAsc ? SortState.UserIdDesc : SortState.UserIdAsc;
            ViewData["ProjectNameSort"] = sort == SortState.ProjectNameAsc ? SortState.ProjectNameDesc : SortState.ProjectNameAsc;
            ViewData["TaskNameSort"]    = sort == SortState.TaskNameAsc ? SortState.TaskNameDesc : SortState.TaskNameAsc;
            ViewData["NoteSort"]        = sort == SortState.NoteAsc ? SortState.NoteDesc : SortState.NoteAsc;
            ViewData["HourSort"]        = sort == SortState.HourAsc ? SortState.HourDesc : SortState.HourAsc;
            ViewData["DataTimeSort"]    = sort == SortState.DataTimeAsc ? SortState.DataTimeDesc : SortState.DataTimeAsc;
            return View("List", SortBy(sort, records));
        }

        // GET: Record/Create
        public ActionResult Create()
        {
            int userId = users.GetUserIdByName(User.Identity.Name);
            var userProjects = projects.GetUserProjects(userId, (int)ProjectRole.Employee);
            List<ProjectTaskDTO> tasks = new List<ProjectTaskDTO>();
            foreach (var proj in userProjects)
            {
                tasks.AddRange(projects.GetTasks(proj.Id).ToList());
            }
            return View(new ProjectRecordViewModel()
            {
                Tasks = tasks,
                Projects = userProjects
            });
        }

        public ActionResult GetTaskList(int id)
        {
            int userId = users.GetUserIdByName(User.Identity.Name);
            var userProjects = projects.GetUserProjects(userId, (int)ProjectRole.Employee);
            List<ProjectTaskDTO> tasks = new List<ProjectTaskDTO>();
            tasks.AddRange(projects.GetTasks(id).ToList());
            return View(new ProjectRecordViewModel()
            {
                Tasks = tasks,
                Projects = userProjects
            });
        }
        
        // POST: Record/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectRecordViewModel record)
        {
            int userId = users.GetUserIdByName(User.Identity.Name);
            if (ModelState.IsValid)
            {  
                service.CreateRecord(
                    new RecordDTO()
                    {
                        UserId = userId,
                        TaskId = record.TaskId,
                        Hours = record.Hours,
                        Note = record.Note,
                        RecordDateTime = DateTime.Parse(record.Date + ',' + record.Time, new CultureInfo("ru", false)),
                        ProjectId = record.ProjectId
                    }
                );

                return RedirectToAction(nameof(UserList));
            }
            var userProjects = projects.GetUserProjects(userId, (int)ProjectRole.Employee);
            record.Projects = userProjects;
            return View(record);
        }

        // GET: Record/Edit/{id}
        public ActionResult Edit(int id)
        {
            RecordDTO record = service.GetRecord(id);
            int userId = users.GetUserIdByName(User.Identity.Name);
            return View(
                new RecordViewModel()
                {
                    Id = record.Id,
                    UserId = userId,
                    TaskId = record.TaskId,
                    Hours = record.Hours,
                    Note = record.Note,
                    TaskName = projects.GetTask(record.TaskId).Name,
                    ProjectName = projects.GetProject(record.ProjectId).Name,
                    RecordDateTime = record.RecordDateTime,
                    Date = record.RecordDateTime.Date.ToShortDateString(),
                    Time = record.RecordDateTime.TimeOfDay.ToString(),
                    ProjectId = record.ProjectId
                }
            );
        }

        // POST: Record/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecordViewModel record)
        {
            if (ModelState.IsValid)
            {
                int userId = users.GetUserIdByName(User.Identity.Name);
                service.UpdateRecord(
                    new RecordDTO()
                    {
                        Id = record.Id,
                        UserId = userId,
                        ProjectId = record.ProjectId,
                        TaskId = record.TaskId,
                        Hours = record.Hours,
                        Note = record.Note,
                        RecordDateTime = DateTime.Parse(record.Date + ',' + record.Time,new CultureInfo("en", false))
                    }
                );

                return RedirectToAction(nameof(UserList));
            }

            return View(record);
        }

        // GET: Record/Delete/{id}
        public ActionResult Delete(int id)
        {
            RecordDTO record = service.GetRecord(id);

            return View(
                new RecordViewModel()
                {
                    UserId = record.UserId,
                    TaskId = record.TaskId,
                    TaskName = projects.GetTask(record.TaskId).Name,
                    ProjectName = projects.GetProject(record.ProjectId).Name,
                    Hours = record.Hours,
                    Note = record.Note,
                    RecordDateTime = record.RecordDateTime,
                    ProjectId = record.ProjectId
                }
            );
        }

        // POST: Record/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            service.DeleteRecord(id);

            return RedirectToAction(nameof(UserList));
        }
    }
}