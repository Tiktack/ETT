using ETT.Storage.Entities.Projects;
using System;
using System.ComponentModel.DataAnnotations;
namespace ETT.Storage.Entities
{
    public class Record
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }

        [Required]
        public double Hours { get; set; }
        public string Note { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
