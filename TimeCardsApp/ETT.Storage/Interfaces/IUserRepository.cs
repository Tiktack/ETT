using ETT.Storage.Entities;
using System;
using System.Collections.Generic;

namespace ETT.Storage.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByName(string Name);
        IEnumerable<User> GetFilteredUsers(Func<User, bool> predicate, int count, int skip);
        User GetUserByEmail(string email);
    }
}
