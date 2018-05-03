using ETT.Web.Models;
using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ETT.Web.Util.Extensions;

namespace ETT.Web.Util.Initializers
{
    public class UserRolesInitializer
    {
        private static UserManager<User> _userManager;
        private static RoleManager<Role> _roleManager;        

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            // admin login and pass
            string adminEmail = "ad1min@mail.com";
            string password = "admin123";

            // adding roles
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new Role("user"));
            }
            if (await roleManager.FindByNameAsync("moderator") == null)
            {
                await roleManager.CreateAsync(new Role("moderator"));
            }
            // adding admin
            AddingUser admin = new AddingUser { Password = "admin123", UserName = "ad1min@mail.com" };
            IdentityResult result = await AddUserAccount(admin);
            if (result.Succeeded)
            {
                await AppointRoleToUser(admin, "admin");
            }

            // adding other users
            List<AddingUser> addingUsers = new List<AddingUser>
            {
                new AddingUser{ UserName = "mail1@mail.ru", Password = "pass1234" },
                new AddingUser{ UserName = "mail2@mail.ru", Password = "pass1234" },
                new AddingUser{ UserName = "mail3@mail.ru", Password = "pass1234" },
            };
            foreach (var item in addingUsers)
            {
                IdentityResult resultOfAdd = await AddUserAccount(item);
                if (resultOfAdd.Succeeded)
                {
                    await AppointRoleToUser(item, "user");
                }
            }
        }

        private static async Task<IdentityResult> AddUserAccount(AddingUser user)
        {
            IdentityResult result;
            User addedUser = new User { Email = user.UserName, UserName = user.UserName };
            if (await _userManager.FindByNameAsync(user.UserName) == null)
            {
                result = await _userManager.CreateAsync(addedUser, user.Password);
            }
            else
            {
                IdentityError identityError = new IdentityError { Description = "User don't adding" };
                result = IdentityResult.Failed(identityError);
            }
            return result;
        }

        private static async Task AppointRoleToUser(AddingUser user, string role)
        {            
            User userForRole = await _userManager.FindByEmailAsync(user.UserName);
            await _userManager.AddToRolesNVAsync(userForRole, new string[] { role });
        }

        struct AddingUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
