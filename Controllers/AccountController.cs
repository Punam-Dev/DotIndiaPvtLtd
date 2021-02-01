using DotIndiaPvtLtd.Dtos;
using DotIndiaPvtLtd.Models;
using DotIndiaPvtLtd.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UserLogInDto userLogInDto, string returnUrl)
        {
            var user = await accountRepository.GetUserByUserNameAndPassword(userLogInDto.UserName, userLogInDto.Password);
            if (user != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, user.UserName));

                string[] roles = user.UserRoles.Split(",");

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = false;

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                TempData["UserID"] = user.UserID;

                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("error", "Invalid username or password");
            return View();
        }

        [HttpGet]
        [Route("Account/SignUp/{id}")]
        public async Task<IActionResult> SignUp(string id)
        {
            // fetch user by id. that no, name and email will go in agreement
            var user = await accountRepository.GetUserByID(id);
            if (user != null)
            {
                return View(user);
            }
            return View();
        }

        [HttpPost]
        [Route("Account/SignUp/{id}")]
        public async Task<IActionResult> SignUp(string id, Users users)
        {
            // fetch user by id. that no, name and email will go in agreement
            var user = await accountRepository.GetUserByID(id);
            if (user != null)
            {
                user.ActivationDate = DateTime.Now;
                user.OTP = null;
                await accountRepository.UpdateUser(user);

                return RedirectToAction("LogIn");
            }
            ModelState.AddModelError("error", "Invalid user");
            return View();
        }

        [HttpGet]
        [Route("Account/VerifyOTP/{id}")]
        public async Task<IActionResult> VerifyOTP(string id)
        {
            // fetch user by id. that no, name and email will go in agreement
            var user = await accountRepository.GetUserByID(id);
            if (user != null)
            {
                if (string.IsNullOrEmpty(Convert.ToString(user.OTP)) || user.OTP == 0)
                {
                    if (user.ActivationDate == null || string.IsNullOrEmpty(Convert.ToString(user.ActivationDate)))
                    {
                        return RedirectToAction("LogIn");
                    }

                    return RedirectToAction("SignUp", new { id = id });
                }
                return View();
            }
            ModelState.AddModelError("error", "Invalid user");
            return View();
        }

        [HttpPost]
        [Route("Account/VerifyOTP/{id}")]
        public async Task<IActionResult> VerifyOTP(VerifyOTPDto verifyOTPDto, string id)
        {
            var user = await accountRepository.GetUserByID(id);
            if (user != null)
            {
                if (user.OTP == verifyOTPDto.OTP)
                {
                    user.OTP = null;
                    await accountRepository.UpdateUser(user);

                    return RedirectToAction("SignUp", new { id = id });
                }
                return View(verifyOTPDto);
            }
            ModelState.AddModelError("error", "Invalid user");
            return View();
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }
            return RedirectToAction("LogIn");
        }
    }
}
