using ETT.Logic.Interfaces;
using ETT.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Util.Extensions
{
    public static class ControlleExtensions
    {
        public  static int GetCurrentUserId(this Controller controller)
        {
            string name = controller.User.Identity.Name;
            return new UserService().GetUserIdByName(name);
        }       
    }
}
