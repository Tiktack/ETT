using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Users
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Company { get; set; }
        public string Position { get; set; }

        public string About { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string AvatarPath { get; set; }
    }
}
