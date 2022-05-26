using EStore.Application.Services.Users.Commands.EditUser;
using EStore.Application.Services.Users.Commands.RegisterUser;
using EStore.Application.Services.Users.Commands.RemoveUser;
using EStore.Application.Services.Users.Commands.UserStatusChange;
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
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserStatusChangeService _userStatusChangeService;
        private readonly IEditUserService _editUserService;

        public UsersController(IGetUsersService getUsersService, IGetRolesService getRolesService, IRegisterUserService registerUserService, IRemoveUserService removeUserService,IUserStatusChangeService userStatusChangeService, IEditUserService editUserService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
            _userStatusChangeService = userStatusChangeService;
            _editUserService = editUserService;
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
    
        [HttpPost]
        public IActionResult Delete(int userId)
        {
            return Json(_removeUserService.Execute(userId));
        }

        [HttpPost]
        public IActionResult UserStateChange(int userId)
        {
           return Json(_userStatusChangeService.Execute(userId));
        }

        [HttpPost]
        public IActionResult Edit(int userId,string fullName)
        {
            return Json(_editUserService.Execute(new RequsetEditUserDto() { 
            Id=userId,
            FullName=fullName
            }));

        }

    }
}
