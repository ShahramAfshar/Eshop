using EndPoint.Site.Utilities;
using EStore.Application.Services.Carts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class Cart: ViewComponent
    {
        private readonly ICartService _cartService;
      

        public Cart(ICartService cartService)
        {
            _cartService = cartService;
         
        }


        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            CookiesManeger cookiesManeger = new CookiesManeger();
            var model = _cartService.GetMyCart(cookiesManeger.GetBrowserId(HttpContext), userId).Data;


            return View(viewName: "Cart", model:model);
        }
    }
}
