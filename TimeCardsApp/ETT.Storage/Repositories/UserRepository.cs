using System;
using System.Collections.Generic;
using ETT.Storage.Context;
using ETT.Storage.Entities;
using ETT.Storage.Interfaces;
using System.Linq;
using ETT.Storage.Infrastucture;

namespace ETT.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(User item) => context.Users.Add(item);

        public void Delete(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
            }
        }

        public User Get(int id)
        {
            User user = context.Users.Find(id);
            return user;
        }

        public IEnumerable<User> GetAll() => context.Users;

        public IEnumerable<User> GetFilteredUsers(Func<User, bool> predicate, int count = 10, int skip = 0)
        {
            IEnumerable<User> users = context.Users.Where(predicate).OrderBy(x => x.Email).Skip(skip * count).Take(count);
            return users;
        }

        public User GetUserByName(string Name)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == Name);
            return user;
        }

        public void Update(User item)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == item.Id);
            Mappers.MapUserToUserForUpdate(item, user);
            context.SaveChanges();
            //context.Entry(user).State = EntityState.Modified;            
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(user => user.Email == email);
        }
    }
}
