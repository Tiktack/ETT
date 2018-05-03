using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ETT.Web.Models.Users
{
    public class UserUpdateViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "AccountNickname")]
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        [Display(Name = "AccountFirstname")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "AccountLastname")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "AccountCompany")]
        [DataType(DataType.Text)]
        public string Company { get; set; }

        [Display(Name = "AccountPosition")]
        [DataType(DataType.Text)]
        public string Position { get; set; }

        [Display(Name = "AccountAbout")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Display(Name = "AccountDateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string AvatarPath { get; set; }

        public string Email { get; set; }
    }
}
