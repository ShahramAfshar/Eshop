using EStore.Application.Services.Finance.Queries.GetRequestPayForAdmin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestPaysController : Controller
    {
        private readonly IGetRequestPayForAdminService _getRequestPayForAdminService;

        public RequestPaysController(IGetRequestPayForAdminService getRequestPayForAdminService)
        {
           _getRequestPayForAdminService= getRequestPayForAdminService;
        }

        public IActionResult Index()
        {
            var model = _getRequestPayForAdminService.Execute().Data;
            return View(model);
        }
    }
}
