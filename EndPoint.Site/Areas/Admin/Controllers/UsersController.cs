using EStore.Application.Services.Users.Commands.RegisterUser;
using EStore.Application.Services.Users.Queries.GetRoles;
using EStore.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;



        public UsersController(
            IGetUsersService getUsersService, IGetRolesService getRolesService, IRegisterUserService registerUserService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
        }

        public IActionResult Index(string searchKey, int page = 1)
        {
            return View(_getUsersService.Execute(new RequstGetUserDTO()
            {
                Page = page,
                SearchKey = searchKey
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(string email, string fullName,int roleId,string password,string rePassword)
        {
            var result = _registerUserService.Execute(new RequestRegisterUserDTO() { 
            Email=email,
            FullName=fullName,
             Roles= new List<RolesInRegisterUserDTO>()
             { 
              new RolesInRegisterUserDTO()
              {
                  Id=roleId
              } 
             },
             Password=password,
             RePassword=rePassword
            });

            return Json(result);
        }
    }
}
