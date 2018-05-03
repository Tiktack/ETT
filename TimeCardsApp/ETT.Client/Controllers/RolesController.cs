using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETT.Web.Models;
using ETT.Web.Models.Users;
using ETT.Web.Util.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETT.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : AppBaseController
    {
        RoleManager<Role> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View(_roleManager.Roles.OrderBy(x => x.Name).ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (_roleManager.Roles.Any(x => x.Name == name))
                {
                    ModelState.AddModelError(string.Empty, "Данная роль содержиться в базе");
                    return View("Create", name);
                }

                IdentityResult result = await _roleManager.CreateAsync(new Role(name));
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
            return View("Create", name);
        }

        [HttpGet]
        public IActionResult Delete(DeleteRoleViewModel deleteRoleViewModel)
        {
            return View(deleteRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Role role = await _roleManager.FindByIdIntAsync(id);
            if (role.NormalizedName.CompareTo("ADMIN") == 0)
            {
                return RedirectToAction("Index");
            }
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserList(int page = 1, string PageFilter = "")
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
            RolesUsersListViewModel viewModel = new RolesUsersListViewModel
            {
                PageViewModel = pageViewModel,
                Users = items,
                FilterName = PageFilter
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int userId, int page, string filter)
        {
            User user = await _userManager.FindByIdIntAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                    PageNumber = page,
                    FilterString = filter
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int userId, List<string> roles, int page, string PageFilter)
        {
            User user = await _userManager.FindByIdIntAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                IdentityResult resultAdd = await _userManager.AddToRolesNVAsync(user, addedRoles);

                IdentityResult resultRemove = await _userManager.RemoveFromRolesNVAsync(user, removedRoles);

                return RedirectToAction("UserList", new { page = page, PageFilter = PageFilter });
            }

            return NotFound();
        }
    }
}