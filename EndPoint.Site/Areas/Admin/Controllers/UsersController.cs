using EStore.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IGetUsersService getUsersService)
        {
            _getUsersService = getUsersService;
        }

        public IActionResult Index(string searchKey , int page=1)
        {
            return View(_getUsersService.Execute( new RequstGetUserDTO() { 
            Page=page,SearchKey=searchKey
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
