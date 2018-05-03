using ETT.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETT.Web.Models.Projects.User
{
    public class ListViewModel
    {
        public int ProjectId { get; set; }
        public bool Admin { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<ProjectRole> Roles { get; } = Enum.GetValues(typeof(ProjectRole)).Cast<ProjectRole>();
    }
}
