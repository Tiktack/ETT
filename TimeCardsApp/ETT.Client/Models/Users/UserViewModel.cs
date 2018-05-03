using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models.Users
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
