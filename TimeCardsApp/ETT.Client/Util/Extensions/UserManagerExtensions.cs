using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Util.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> FindByIdIntAsync(this UserManager<User> userManager, int id)
        {
            IQueryable<User> users = userManager.Users;
            User user = await users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        /// <summary>
        /// Update without validation
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<IdentityResult> UpdateNVAsync(this UserManager<User> userManager, User user)
        {
            var validators = PopValidators(userManager);
            var result = await userManager.UpdateAsync(user);
            PushValidators(userManager, validators);            
            return result;
        }

        public static async Task<IdentityResult> AddToRolesNVAsync(this UserManager<User> userManager, User user, IEnumerable<string> roles)
        {
            var validators = PopValidators(userManager);
            var result = await userManager.AddToRolesAsync(user, roles);
            PushValidators(userManager, validators);
            return result;
        }

        public static async Task<IdentityResult> RemoveFromRolesNVAsync(this UserManager<User> userManager, User user, IEnumerable<string> roles)
        {
            var validators = PopValidators(userManager);
            var result =  await userManager.RemoveFromRolesAsync(user, roles);
            PushValidators(userManager, validators);
            return result;
        }

        public static async Task<IdentityResult> ChangePasswordNVAsync(this UserManager<User> userManager, User user, string oldPassword, string newPassword)
        {
            var validators = PopValidators(userManager);
            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            PushValidators(userManager, validators);
            return result;
        }

        private static IList<IUserValidator<User>> PopValidators(UserManager<User> userManager)
        {
            IList<IUserValidator<User>> validators = new List<IUserValidator<User>>();
            foreach (var item in userManager.UserValidators)
            {
                validators.Add(item);
            }
            userManager.UserValidators.Clear();
            return validators;
        }

        private static void PushValidators(UserManager<User> userManager, IList<IUserValidator<User>> validators)
        {
            foreach (var item in validators)
            {
                userManager.UserValidators.Add(item);
            }
        }
    }
}
