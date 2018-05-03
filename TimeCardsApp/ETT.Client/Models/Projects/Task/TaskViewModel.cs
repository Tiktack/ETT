using System;
using System.ComponentModel.DataAnnotations;

namespace ETT.Web.Models.Projects.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public int ProjectId { get; set; }
    }
}
