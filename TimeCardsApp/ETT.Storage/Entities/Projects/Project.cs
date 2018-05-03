using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETT.Storage.Entities.Projects
{
    [Table(name: "Projects", Schema = "Project")]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask> ();
        public ICollection<ProjectUser> Users { get; set; } = new List<ProjectUser> ();
    }
}
