using EndPoint.Site.Models.ViewModel;
using EStore.Application.Services.Users.Commands.LoginUser;
using EStore.Application.Services.Users.Commands.RegisterUser;
using EStore.Common.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly ILoginUserService _loginUserService;

        public AuthenticationController(IRegisterUserService registerUserService, ILoginUserService loginUserService)
        {
            _registerUserService = registerUserService;
            _loginUserService = loginUserService;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel request)
        {
            //با فلونت ولیدیتور ها بنویسیم بهتره
            if (string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName)
              )
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا فیلدها را کامل کنید"
                });
            }
            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "در حال حاضر نمی توان ثیت نام مجدد انجام داد"
                });
            }

            if (request.Password != request.RePassword)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کلمه عبور و تکرار ان یکسان نیست"
                });
            }

            if (request.Password.Length < 8)
            {
                return Json(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کلمه عبور نمی توان کمتر از 8 کاراکتر باشد"
                });
            }
            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new ResultDto { IsSuccess = true, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }

            var signupResult = _registerUserService.Execute(new RequestRegisterUserDTO()
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePassword = request.RePassword,
                Roles = new List<RolesInRegisterUserDTO>() {
               new RolesInRegisterUserDTO(){Id=3}
            }
            });


            if (signupResult.IsSuccess)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email,request.Email),
                    new Claim(ClaimTypes.Name,request.FullName),
                    new Claim(ClaimTypes.Role,"Customer"),

                };

                //لاگین کاربر را انجام دادیم
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(principal, properties);

                return Json(signupResult);

            }


            return View();
        }

        [HttpGet]
        public IActionResult Signin(string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string email, string password, string url = "/")
        {
            var signupResult = _loginUserService.Execute(email, password);

            if (signupResult.IsSuccess)
            {
                var claims = new List<Claim>() {

                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, signupResult.Data.Name),
                new Claim(ClaimTypes.Role,signupResult.Data.Roles),

                };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };

                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signupResult);


        }

        [HttpGet]
        public IActionResult Signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}
