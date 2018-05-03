using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Projects
{
    public class DetailsViewModel : ProjectViewModel
    {
        public bool Owner { get; set; }
        public bool Admin { get; set; }
    }
}
