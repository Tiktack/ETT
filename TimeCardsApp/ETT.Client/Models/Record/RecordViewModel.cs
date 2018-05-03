using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ETT.Web.Models.Record
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Hours")]
        [Range(0.1,15,ErrorMessage = "WorkTime")]
        [Required(ErrorMessage = "RequiredHours")]
        public double Hours { get; set; }
       
        [Display(Name = "Note")]
        [MinLength(3,ErrorMessage ="MinLengthNote")]
        [Required(ErrorMessage ="RequiredNote")]
        public string Note { get; set; }

        [Display(Name = "StartDate")]
        [Required(ErrorMessage = "RequiredStartDate")]
        public string Date { get; set; }

        [Display(Name = "StartTime")]
        [Required(ErrorMessage = "RequiredStartTime")]
        public string Time { get; set; }

        [Display(Name = "CreationTime")]
        public DateTime RecordDateTime { get; set; }

        [RegularExpression(@"[1-9][0-9]{0,}",ErrorMessage = "RequeredProjectName")]
        [Display(Name = "ProjectName")]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [RegularExpression(@"[1-9][0-9]{0,}", ErrorMessage = "RequeredTaskName")]
        [Display(Name = "TaskName")]
        public int TaskId { get; set; }

        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
    }
}
