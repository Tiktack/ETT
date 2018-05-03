using ETT.Utils.Enums;

namespace ETT.Web.Models.Projects.User
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string Email { get; set; }
        public ProjectRole Role { get; set; }
    }
}
