using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETT.Web.Models.Users
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[Required]
        public string NickName { get; set; }

        public string Company { get; set; }
        public string Position { get; set; }

        public string About { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
