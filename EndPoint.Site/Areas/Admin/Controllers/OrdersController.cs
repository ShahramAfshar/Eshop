using EStore.Application.Services.Order.Queries.GetOrdersForAdmin;
using EStore.Domain.Entities.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IGetOrdersForAdminService _getOrdersForAdminService;

        public OrdersController(IGetOrdersForAdminService getOrdersForAdminService)
        {
            _getOrdersForAdminService = getOrdersForAdminService;
        }

        public IActionResult Index(OrderState orderState)
        {
            var model = _getOrdersForAdminService.Execute(orderState).Data;
            return View(model);
        }
    }
}
