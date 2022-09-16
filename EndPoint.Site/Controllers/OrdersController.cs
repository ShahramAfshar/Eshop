using EndPoint.Site.Utilities;
using EStore.Application.Services.Order.Queries.GetUserOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
  //  [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGetUserOrdersService _getUserOrdersService;
        public OrdersController(IGetUserOrdersService getUserOrdersService)
        {
            _getUserOrdersService = getUserOrdersService;
        }

        public IActionResult Index()
        {
            int userId = ClaimUtility.GetUserId(User).Value;
           var model= _getUserOrdersService.Execute(userId).Data;

            return View(model);
        }
    }
}
