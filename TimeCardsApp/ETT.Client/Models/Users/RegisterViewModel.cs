using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Users
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "EmailError")]
        [Display(Name = "EmailProp")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordError")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordProp")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PasswordConfirmError")]
        [Compare("Password", ErrorMessage = "PasswordCompareError")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirmProp")]
        public string PasswordConfirm { get; set; }
    }
}
