using System;
using System.Collections.Generic;
using System.Text;

namespace ETT.Logic.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string NickName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Company { get; set; }
        public string Position { get; set; }

        public string About { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PathOfAvatarImage { get; set; }
    }
}
