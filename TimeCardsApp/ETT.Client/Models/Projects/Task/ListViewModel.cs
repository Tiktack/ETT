using System.Collections.Generic;

namespace ETT.Web.Models.Projects.Task
{
    public class ListViewModel
    {
        public int projectId { get; set; }
        public bool Manager { get; set; }

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}
