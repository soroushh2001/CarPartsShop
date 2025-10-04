using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Accounts;
using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Domain.Entities.Account;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarPartsShop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class UsersController : AdminBaseController
    {

        #region Constructor

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager, IUserService userService, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        #endregion

        #region FilterUsers

        public async Task<IActionResult> Index(UserFilterSpecification specification)
        {

            ViewBag.OrderBySelectList = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "Newest",
                Text = "جدیدترین",
                Selected = specification.OrderBy == "Newest"
            },
            new SelectListItem()
            {
                Value = "Oldest",
                Text = "قدیمیترین",
                Selected = specification.OrderBy == "Oldest"
            }
        }.ToList();

            var roleList = await _roleManager.Roles.ToListAsync();

            ViewBag.Roles = roleList.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Name,
                Selected = x.Name == specification.Role
            }).ToList();
            var filter = await _userService.FilterUsersAsync(specification);
            return View(filter);
        }


        #endregion

        #region ManageUserRoles


        [HttpGet]
        public async Task<IActionResult> LoadManageUserRolesModal(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            var currentRoleNames = await _userManager.GetRolesAsync(user) as List<string>;

            return PartialView("_ManageUserRoles", new ManageUserRolesViewModel
            {
                SelectedRoles = currentRoleNames,
                Email = user.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitManageUserRolesModal(ManageUserRolesViewModel manage)
        {

            var user = await _userManager.FindByEmailAsync(manage.Email);
            if (user == null)
                return JsonHelper.JsonResponse(404, "NotFound");

            if (manage.SelectedRoles == null)
            {
                return JsonHelper.JsonResponse(400, "لطفا حداقل یک نقش را انتخاب کنید");
            }

            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            var result = await _userManager.AddToRolesAsync(user, manage.SelectedRoles);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");
            }
            return JsonHelper.JsonResponse(400, "عملیات با شکست مواجه شد");
        }



        #endregion

        #region LockUnlock

        public async Task<IActionResult> LockUnlock(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100));
            }
            TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
            return RedirectToAction("Index");
        }

        #endregion

        #region ChangeUserPassword

        [HttpGet]
        public async Task<IActionResult> LoadChangeUserPasswordModal(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound();


            return PartialView("_ChangeUserPassword", new ChangePasswordInAdminViewModel
            {
                Email = email,
            });
        }


        [HttpPost]
        public async Task<IActionResult> SubmitChangeUserPasswordModal(ChangePasswordInAdminViewModel change)
        {
            var user = await _userManager.FindByEmailAsync(change.Email);

            if (user == null)
            {
                return JsonHelper.JsonResponse(404, "NotFound");
            }

            await _userManager.RemovePasswordAsync(user);

            var result = await _userManager.AddPasswordAsync(user, change.Password);

            if (result.Succeeded)
            {
                return JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد");
            }


            foreach (var error in result.Errors)
            {
                return JsonHelper.JsonResponse(403, error.Description);
            }

            return JsonHelper.JsonResponse(403, "عملیات با شکست مواجه شد");
        }

        #endregion
    }
}
