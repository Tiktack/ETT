using ETT.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETT.Logic.Interfaces
{
    public interface IUserService
    {
        //void CreateUser(UserDTO user);
        UserDTO GetUser(int? id);
        IEnumerable<UserDTO> GetUsers();
        int GetUserIdByName(string name);
        IEnumerable<UserDTO> SearchUsersByEmail(string Email, int limit);
        void UpdateUserAccountData(UserDTO userDTO);
        UserDTO GetUserByEmail(string email);
        void Dispose();
    }
}
