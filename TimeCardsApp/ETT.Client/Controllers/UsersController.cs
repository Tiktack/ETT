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

namespace ETT.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : AppBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserService _userService;

        public UsersController(UserManager<User> userManager,
            IFileService fileService,
            IHostingEnvironment hostingEnvironment,
            IUserService userService)
        {
            _userManager = userManager;
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int page = 1, string PageFilter = "")
        {
            int pageSize = 5;

            IQueryable<User> users = null;
            if (!String.IsNullOrEmpty(PageFilter))
            {
                users = _userManager.Users.Where(x => x.Email.Contains(PageFilter));
            }
            else
            {
                users = _userManager.Users;
            }

            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            UsersIndexViewModel viewModel = new UsersIndexViewModel
            {
                PageViewModel = pageViewModel,
                Users = items,
                FilterName = PageFilter
            };

            return View(viewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //adding user role
                    await _userManager.AddToRoleAsync(user, "user");
                    return RedirectToAction("Index");
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

        public async Task<IActionResult> Edit(string id)
        {
            // check
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            //EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email };
            EditUserViewModel model = Mappers.MapUserToUserEditVM(user);
            model.AvatarPath = _fileService.GetAvatarPath(user.Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = Mappers.MapUserEditVMToUserDTOEditVM(model);

                _userService.UpdateUserAccountData(userDTO);

                return RedirectToAction("Index");
                //User user = await _userManager.FindByIdIntAsync(model.Id);

                //if (userDTO != null)
                //{

                //    //if (model.Email.CompareTo(user.Email) == 0) return RedirectToAction("Index");
                //    //user.Email = model.Email;
                //    //user.UserName = model.Email;

                //    //var result = await _userManager.UpdateAsync(user);
                //    //if (result.Succeeded)
                //    //{
                //    //    return RedirectToAction("Index");
                //    //}
                //    //else
                //    //{
                //    //    foreach (var error in result.Errors)
                //    //    {
                //    //        ModelState.AddModelError(string.Empty, error.Description);
                //    //    }
                //    //}
                //}
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, bool confirm = false)
        {
            User user = await _userManager.FindByIdIntAsync(id);

            if (user != null)
            {
                if (confirm)
                {
                    if (user.NormalizedUserName == "ad1min@mail.com".ToUpper())
                    {
                        return RedirectToAction("Index");
                    }

                    IdentityResult result = await _userManager.DeleteAsync(user);
                }
                else
                {
                    ViewBag.Id = id;
                    ViewBag.Name = user.UserName;
                    return View();
                }

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdIntAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordNVAsync(user, model.OldPassword, model.NewPassword);


                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> RemoveIMG()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                string id = "";
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<PostData>(requestBody);
                        if (obj != null)
                        {
                            id = obj.id;
                        }
                    }

                }
                _fileService.RemoveAvatarImage(int.Parse(id), _hostingEnvironment.WebRootPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("complete");
        }

        public class PostData
        {
            public string id { get; set; }
        }
    }
}