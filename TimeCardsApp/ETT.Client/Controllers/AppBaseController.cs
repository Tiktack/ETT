using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ETT.Logic.Services;
using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ETT.Web.Util.Extensions;

namespace ETT.Web.Controllers
{
    public class AppBaseController : Controller
    {
        /// <summary>
        /// Return id of the user where is authenticated on the site
        /// </summary>
        public int? CurrentUserId
        {
            get
            {
                return this.User.Identifier();
            }
        }
    }
}