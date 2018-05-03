using ETT.Utils.Enums;

namespace ETT.Logic.DTO.Projects
{
    public class ProjectUserDTO
    {
        public int UserId { get; set; }

        public string Email { get; set; }
        public ProjectRole Role { get; set; }
    }
}
