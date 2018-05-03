using ETT.Logic.DTO.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Report
{
    public class ReportViewModel
    {
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
        public IEnumerable<ProjectDTO> Projects { get; set; }
        public IEnumerable<ProjectTaskDTO> Tasks { get; set; }
        public IEnumerable<ProjectUserDTO> Users { get; set; }
        public IEnumerable<ReportRecordsViewModel> Records { get; set; }
    }
}
