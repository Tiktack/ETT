using System.ComponentModel.DataAnnotations;

namespace ETT.Web.Models.Projects
{
    public class ProjectViewModel
    {
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
