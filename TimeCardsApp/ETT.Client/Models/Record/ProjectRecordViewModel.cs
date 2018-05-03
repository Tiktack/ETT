using ETT.Logic.DTO.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Record
{
    public class ProjectRecordViewModel:RecordViewModel
    {
        public IEnumerable<ProjectDTO> Projects { get; set; }
        public IEnumerable<ProjectTaskDTO> Tasks { get; set; }
    }
}
