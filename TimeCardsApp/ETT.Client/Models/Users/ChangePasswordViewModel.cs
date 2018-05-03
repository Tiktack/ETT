using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Users
{
    public class ChangePasswordViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходим новый пароль")]
        [DataType(DataType.Text)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Необходим старый пароль")]
        [DataType(DataType.Text)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }
    }
}
