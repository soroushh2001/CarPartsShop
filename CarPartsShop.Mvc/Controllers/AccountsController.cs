using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Accounts;
using CarPartsShop.Domain.Entities.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Application.ViewModels.Accounts;
using System.Net;
using CarPartsShop.Mvc.Helpers;

namespace CarPartsShop.Mvc.Controllers
{
    public class AccountsController : Controller
    {

        #region Constructor

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICaptchaValidator _captchaValidator;


        public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, ICaptchaValidator captchaValidator)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _captchaValidator = captchaValidator;
        }

        #endregion

        #region Register

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(register.Captcha))
            {
                TempData[ToastrMessages.ErrorMessage] = "اعتبار سنجی Captcha با خطا مواجه شد لطفا مجدد تلاش کنید .";
                return View(register);
            }
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var user = new ApplicationUser
            {
                Email = register.Email,
                UserName = register.Email,
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var domain = _configuration.GetValue<string>("Domain");
                var encodedCode = WebUtility.UrlEncode(code);
                var callBackUrl = $"{domain}/Accounts/ConfirmEmail?email={user.Email}&token={encodedCode}";
                user.Email.SendEmail("ایمیل فعالسازی", $"<a href='{callBackUrl}'>فعالسازی</a>");
                await _userManager.AddToRoleAsync(user, RoleConstants.User);
                TempData[ToastrMessages.SuccessMessage] = "ثبت نام با موفقیت انجام شد";
                TempData[ToastrMessages.InfoMessage] = "ایمیلی حاوی لینک فعالسازی برای شما ارسال شد";
                return RedirectToAction("Login", "Accounts");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(register);
        }

        #endregion

        #region ConfirmEmail

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData[ToastrMessages.SuccessMessage] = "فعالسازی حساب با موفقیت انجام شد";
                return RedirectToAction("Login");

            }
            return NotFound();
        }

        #endregion

        #region Login

        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginUserViewModel();
            if (returnUrl != null)
                model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserViewModel login)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
            {
                TempData[ToastrMessages.ErrorMessage] = "اعتبار سنجی Captcha با خطا مواجه شد لطفا مجدد تلاش کنید .";
                return View(login);
            }

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");
                return View(login);
            }

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (result.Succeeded)
            {
                TempData[ToastrMessages.SuccessMessage] = "خوش آمدید";
                return !string.IsNullOrEmpty(login.ReturnUrl)
                    ? Redirect(login.ReturnUrl)
                    : RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "حساب کاربری شما به دلیل ورودهای ناموفق متعدد قفل شده است");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "لطفا ایمیل خود را تایید کنید");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");
            }

            return View(login);
        }

        #endregion

        #region Logout

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
            return RedirectToAction("Login", "Accounts");
        }

        #endregion

        #region ForgotPassword

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotUserPasswordViewModel forgot)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(forgot.Captcha))
            {
                TempData[ToastrMessages.ErrorMessage] = "اعتبار سنجی Captcha با خطا مواجه شد لطفا مجدد تلاش کنید .";
                return View(forgot);
            }

            if (!ModelState.IsValid)
            {
                return View(forgot);
            }

            var user = await _userManager.FindByEmailAsync(forgot.Email);
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var domain = _configuration.GetValue<string>("Domain");
                var encodedCode = WebUtility.UrlEncode(code);
                var callBackUrl = $"{domain}/reset-password?token={encodedCode}&email={forgot.Email}";
                forgot.Email.SendEmail("ایمیل فعالسازی", $"<a href='{callBackUrl}'>بازیابی کلمه عبور</a>");
                TempData[ToastrMessages.SuccessMessage] = "ایمیلی حاوی لینک بازیابی کلمه عبور برای شما ارسال شد";
                return RedirectToAction("Login", "Accounts");
            }
            ModelState.AddModelError("", "کاربری با این مشخصات یافت نشد.");
            return View(forgot);
        }

        #endregion

        #region ResetPassword

        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetUserPasswordViewModel
            {
                Email = email,
                Token = token
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetUserPasswordViewModel reset)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(reset.Captcha))
            {
                TempData[ToastrMessages.ErrorMessage] = "اعتبار سنجی Captcha با خطا مواجه شد لطفا مجدد تلاش کنید .";
                return View(reset);
            }

            if (!ModelState.IsValid)
            {
                return View(reset);
            }

            var user = await _userManager.FindByEmailAsync(reset.Email);
            if(user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.Password);
                if (result.Succeeded)
                {
                    TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("Login", "Accounts");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ModelState.AddModelError("", "کاربری با این مشخصات یافت نشد.");

            return View(reset);
        }

        #endregion

    }
}
