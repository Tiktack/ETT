using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETT.Storage.Entities.Projects
{
    [Table(name: "Tasks", Schema = "Project")]
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
