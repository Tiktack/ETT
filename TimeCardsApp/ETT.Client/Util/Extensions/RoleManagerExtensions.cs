using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Util.Extensions
{
    public static class RoleManagerExtensions
    {
        public static async Task<Role> FindByIdIntAsync(this RoleManager<Role> roleManager, int id)
        {
            IQueryable<Role> applicationRoles = roleManager.Roles;
            Role applicationRole = await applicationRoles.FirstOrDefaultAsync(x => x.Id == id);
            return applicationRole;
        }
    }
}
