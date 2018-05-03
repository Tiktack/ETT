using ETT.Logic.DTO;
using ETT.Logic.Interfaces;
using System.Collections.Generic;
using ETT.Storage;
using ETT.Storage.Entities;
using System.Linq;
using ETT.Logic.Managers;

namespace ETT.Logic.Services
{
    public class UserService : IUserService
    {
        private UnitOfWork Database { get; set; }
        private AccountManager usersManager;

        public UserService()
        {
            Database = new UnitOfWork();
            usersManager = new AccountManager();
        }        

        public UserDTO GetUser(int? id) =>
            usersManager.GetUser(id);
        

        public IEnumerable<UserDTO> GetUsers() =>
            usersManager.GetUsers();

        //todo fix this method
        public int GetUserIdByName(string name)
        {
            UserDTO user = usersManager.SearchUsers(x => x.Email == name, 1).ToList()[0];
            return user.UserID;
        }

        public IEnumerable<UserDTO> SearchUsersByEmail(string email, int limit) =>
            usersManager.SearchUsers(x => x.Email.Contains(email), limit);

        public void UpdateUserAccountData(UserDTO userDTO) => usersManager.UpdateUser(userDTO);

        public void Dispose()
        {
            Database.Dispose();
            usersManager.Dispose();
        }

        public UserDTO GetUserByEmail(string email)
        {
            User user = Database.Users.GetUserByEmail(email);
            if (user != default(User))
            {
                return new UserDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    NickName = user.NickName,
                    Email = user.Email,
                    UserID = user.Id,
                };
            }
            return default(UserDTO);
        }

    }
}
