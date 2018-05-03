using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETT.Logic.DTO.Projects;
using ETT.Logic.Interfaces;
using ETT.Utils.Enums;
using ETT.Web.Models.Record;
using ETT.Web.Models.Report;
using Microsoft.AspNetCore.Mvc;

namespace ETT.Web.Controllers
{
    public class ReportController : AppBaseController
    {
        private IRecordService service;
        private IUserService users;
        private IProjectService projects;
        public ReportController(IRecordService recordService, IUserService userService, IProjectService projectService)
        {
            this.service = recordService;
            this.users = userService;
            this.projects = projectService;
        }

        public ActionResult CreateReport()
        {
            int userId = users.GetUserIdByName(User.Identity.Name);
            var userProjects = projects.GetUserProjects(userId, (int)ProjectRole.Manager);
            List<ProjectTaskDTO> tasks = new List<ProjectTaskDTO>();
            List<ProjectUserDTO> usersProj = new List<ProjectUserDTO>();
            foreach (var proj in userProjects)
            {
                tasks.AddRange(projects.GetTasks(proj.Id).ToList());
                usersProj.AddRange(projects.GetUsers(proj.Id).ToList());
            }
            return View(new ReportViewModel()
            {
                Tasks = tasks,
                Projects = userProjects,
                Users = usersProj
            });
        }

        public ActionResult GetTaskUserList(int id)
        {

            int userId = users.GetUserIdByName(User.Identity.Name);
            var userProjects = projects.GetUserProjects(userId, (int)ProjectRole.Manager);
            List<ProjectTaskDTO> tasks = new List<ProjectTaskDTO>();
            List<ProjectUserDTO> usersProj = new List<ProjectUserDTO>();
            tasks.AddRange(projects.GetTasks(id).ToList());
            usersProj.AddRange(projects.GetUsers(id).ToList());
            return View(new ReportViewModel()
            {
                Tasks = tasks,
                Projects = userProjects,
                Users = usersProj
            });
        }
        [HttpGet]
        public ActionResult GetSearchList(int projId, string taskId, string userId/*, SortState sort*/)
        {
            //ViewData["IdSort"] = sort == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            //ViewData["UserNameSort"] = sort == SortState.UserNameAsc ? SortState.UserNameDesc : SortState.UserNameAsc;
            //ViewData["ProjectNameSort"] = sort == SortState.ProjectNameAsc ? SortState.ProjectNameDesc : SortState.ProjectNameAsc;
            //ViewData["TaskNameSort"] = sort == SortState.TaskNameAsc ? SortState.TaskNameDesc : SortState.TaskNameAsc;
            //ViewData["NoteSort"] = sort == SortState.NoteAsc ? SortState.NoteDesc : SortState.NoteAsc;
            //ViewData["HourSort"] = sort == SortState.HourAsc ? SortState.HourDesc : SortState.HourAsc;
            //ViewData["DataTimeSort"] = sort == SortState.DataTimeAsc ? SortState.DataTimeDesc : SortState.DataTimeAsc;
            int[] taskIds=null;
            int[] userIds = null;
            if (taskId!=null)
            { 
            taskIds = taskId.Split(',').Select(x => Int32.Parse(x)).ToArray();
            }
            if (userId != null)
            {
                userIds = userId.Split(',').Select(x => Int32.Parse(x)).ToArray();
            }
            IEnumerable<ReportRecordsViewModel> records;
            List<ReportRecordsViewModel> tempRecords = new List<ReportRecordsViewModel>();
            if (taskId != null && userId != null)
            {
                foreach (var task_id in taskIds)
                {
                    foreach (var user_id in userIds)
                    {
                        tempRecords.AddRange(service.GetRecords().Where(x => x.ProjectId == projId)
                        .Where(x => x.UserId == user_id)
                        .Where(x => x.TaskId == task_id)
                        .Select(record =>
                            new ReportRecordsViewModel()
                            {
                                Id = record.Id,
                                UserId = record.UserId,
                                UserName = users.GetUser(record.UserId).Email,
                                ProjectId = record.ProjectId,
                                TaskId = record.TaskId,
                                TaskName = projects.GetTask(record.TaskId).Name,
                                ProjectName = projects.GetProject(record.ProjectId).Name,
                                Hours = record.Hours,
                                Note = record.Note,
                                RecordDateTime = record.RecordDateTime,
                            }
                        ));
                    }
                }
                #region comment
                //records = service.GetRecords().Where(x => x.ProjectId == projId)
                //    .Where(x => x.UserId == userId)
                //    .Where(x => x.TaskId == taskId[0])
                //    .Select(record =>
                //        new RecordViewModel()
                //        {
                //            Id = record.Id,
                //            UserId = record.UserId,
                //            ProjectId = record.ProjectId,
                //            TaskId = record.TaskId,
                //            TaskName = projects.GetTask(record.TaskId).Name,
                //            ProjectName = projects.GetProject(record.ProjectId).Name,
                //            Hours = record.Hours,
                //            Note = record.Note,
                //            RecordDateTime = record.RecordDateTime,
                //        }
                //    );
                #endregion
            }
            else if (taskId != null)
            {
                foreach (var id in taskIds)
                {
                    tempRecords.AddRange(service.GetRecords().Where(x => x.ProjectId == projId)
                        .Where(x => x.TaskId == id)
                        .Select(record =>
                            new ReportRecordsViewModel()
                            {
                                Id = record.Id,
                                UserId = record.UserId,
                                UserName = users.GetUser(record.UserId).Email,
                                ProjectId = record.ProjectId,
                                TaskId = record.TaskId,
                                TaskName = projects.GetTask(record.TaskId).Name,
                                ProjectName = projects.GetProject(record.ProjectId).Name,
                                Hours = record.Hours,
                                Note = record.Note,
                                RecordDateTime = record.RecordDateTime,
                            }
                        ));
                }
            }
            else if(userId != null)
            {
                foreach (var id in userIds)
                {
                    tempRecords.AddRange(service.GetRecords().Where(x => x.ProjectId == projId)
                    .Where(x => x.UserId == id)
                    .Select(record =>
                        new ReportRecordsViewModel()
                        {
                            Id = record.Id,
                            UserId = record.UserId,
                            UserName = users.GetUser(record.UserId).Email,
                            ProjectId = record.ProjectId,
                            TaskId = record.TaskId,
                            TaskName = projects.GetTask(record.TaskId).Name,
                            ProjectName = projects.GetProject(record.ProjectId).Name,
                            Hours = record.Hours,
                            Note = record.Note,
                            RecordDateTime = record.RecordDateTime,
                        }
                    ));
                }
            }
            else
            {
                tempRecords.AddRange(service.GetRecords().Where(x => x.ProjectId == projId)
                   .Select(record =>
                       new ReportRecordsViewModel()
                       {
                           Id = record.Id,
                           UserId = record.UserId,
                           UserName=users.GetUser(record.UserId).Email,
                           ProjectId = record.ProjectId,
                           TaskId = record.TaskId,
                           TaskName = projects.GetTask(record.TaskId).Name,
                           ProjectName = projects.GetProject(record.ProjectId).Name,
                           Hours = record.Hours,
                           Note = record.Note,
                           RecordDateTime = record.RecordDateTime,
                       }
                   ));
            }
            records = tempRecords;
            //return View(SortBy(sort, records));
            return View(new ReportViewModel()
            {
                Records = records
            });
        }
        public ReportViewModel SortBy(SortState sortParam, IEnumerable<ReportRecordsViewModel> records)
        {
            ReportViewModel model = new ReportViewModel();
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
                case SortState.UserNameAsc:
                    {
                        model.Records = records.OrderBy(s => s.UserName);
                        break;
                    }
                case SortState.UserNameDesc:
                    {
                        model.Records = records.OrderByDescending(s => s.UserName);
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
    }
}