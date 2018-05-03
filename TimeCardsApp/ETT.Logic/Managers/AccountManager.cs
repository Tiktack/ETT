using ETT.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using ETT.Storage.Entities;
using ETT.Logic.DTO;
using ETT.Logic.Infrastructure;
using AutoMapper;

namespace ETT.Logic.Managers
{
    public class AccountManager : IDisposable
    {
        private UnitOfWork Database { get; set; }

        public AccountManager()
        {
            Database = new UnitOfWork();
        }

        public void UpdateUser(UserDTO userForUpdate)
        {
            User user = Mappers.MapUserDTOToUser(userForUpdate);
            Database.Users.Update(user);
        }

        public UserDTO GetUser(int? id)
        {
            UserDTO userDTO = null;
            User user = null;

            if (id.HasValue)
            {
                user = Database.Users.Get(id.Value);

                userDTO = Mappers.MapUserToUserDTO(user);
            }

            return userDTO;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> users = Database.Users.GetAll();
            IEnumerable<UserDTO> userDTOs;

            userDTOs = Mappers.MapUserToUserDTO(users);

            return userDTOs;
        }        



        public IEnumerable<UserDTO> SearchUsers(Func<User, bool> predicate, int limit, int skip = 0)
        {
            IEnumerable<User> users = Database.Users.GetFilteredUsers(predicate, limit, skip);
            IEnumerable<UserDTO> userDTOs; ;

            userDTOs = Mappers.MapUserToUserDTO(users);
            

            return userDTOs;
        }

        public void Dispose() => Database.Dispose();
    }
}
