using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Users
{
    public class ChangeAccountPasswordViewModel
    {
        [Required]        
        public string Email { get; set; }

        [Required(ErrorMessage = "NewPasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPasswordProp")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "NewPasswordRepeatError")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "NewPasswordCompareError")]
        [Display(Name = "NewPasswordRepeatProp")]
        public string NewPasswordRepeat { get; set; }

        [Required(ErrorMessage = "OldPasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "OldPasswordProp")]
        public string OldPassword { get; set; }
    }
}
