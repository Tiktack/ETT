using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ETT.Web.Infrastructure;
using ETT.Logic.Interfaces;
using ETT.Web.Models.Users;
using ETT.Web.Util.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ETT.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ETT.Web.Util.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ETT.Web.Models;
using ETT.Web.Infrastructure;
using ETT.Logic.Interfaces;
using ETT.Logic.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using ETT.Logic.DTO;
using Microsoft.Extensions.Localization;

namespace ETT.Web.Controllers
{
    public class AccountController : AppBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IHostingEnvironment hostingEnvironment,
            IFileService fileService,
            IUserService userService,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
            _userService = userService;
            _sharedLocalizer = sharedLocalizer;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                //string str = "no files";
                return Json(null);
            }

            var filePath = _hostingEnvironment.WebRootPath;
            IFormFile formFile = files[0];

            bool resOfUpload = await _fileService.SetAvatarImageAsync(CurrentUserId, filePath, formFile);

            if (resOfUpload)
            {
                string path = _fileService.GetAvatarPath(CurrentUserId);
                if (path != null) return Json(JsonConvert.SerializeObject(new { Path = path }));
            }

            return Json(null);
        }

        class Data
        {
            public string Path { get; set; }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //adding user role
                    await _userManager.AddToRoleAsync(user, "user");
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string name)
        {
            if (name != null)
            {
                User user = await _userManager.FindByEmailAsync(name);
                if (user.Id != CurrentUserId) return BadRequest();
                UserUpdateViewModel userUpdateViewModel = Mappers.MapUserToUserUpdateVM(user);
                userUpdateViewModel.AvatarPath = _fileService.GetAvatarPath(CurrentUserId);
                return View(userUpdateViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {

                UserDTO userDTO = Mappers.MapUserUpdateVMToUserDTO(model);
                if (userDTO.DateOfBirth?.Date == DateTime.Now.Date) userDTO.DateOfBirth = null;
                _userService.UpdateUserAccountData(userDTO);
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            User user = await _userManager.FindByIdIntAsync(CurrentUserId ?? 0);
            if (user == null)
            {
                return NotFound();
            }

            ChangeAccountPasswordViewModel model = new ChangeAccountPasswordViewModel { Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeAccountPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdIntAsync(CurrentUserId ?? 0);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordNVAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }

            return View(model);
        }
    }
}