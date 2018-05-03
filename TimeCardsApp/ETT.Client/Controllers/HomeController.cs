using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETT.Web.Models;
using ETT.Logic.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace ETT.Web.Controllers
{
    public class HomeController : AppBaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = CurrentUserId ?? 0;
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl,string query)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl+query);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
