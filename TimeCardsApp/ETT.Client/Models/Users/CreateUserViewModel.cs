using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "EmailError")]
        [DataType(DataType.Text)]
        [Display(Name = "EmailProp")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordError")]
        [DataType(DataType.Text)]
        [Display(Name = "PasswordProp")]
        public string Password { get; set; }
    }
}
