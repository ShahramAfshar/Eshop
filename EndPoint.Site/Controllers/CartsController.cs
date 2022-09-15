using EndPoint.Site.Utilities;
using EStore.Application.Services.Carts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
           var userId= ClaimUtility.GetUserId(User);
            CookiesManeger cookiesManeger = new CookiesManeger();
          var cart=  _cartService.GetMyCart(cookiesManeger.GetBrowserId(HttpContext),userId).Data;
            return View(cart);
        }

        public IActionResult AddToCart(int productId)
        {
            CookiesManeger cookiesManeger = new CookiesManeger();
            _cartService.AddToCart(productId,cookiesManeger.GetBrowserId(HttpContext));

            return RedirectToAction("Index");
        }
        public IActionResult Add(int cartItemId)
        {
            _cartService.Add(cartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult LowOff(int cartItemId)
        {
            _cartService.LowOff(cartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int productId)
        {
            CookiesManeger cookiesManeger = new CookiesManeger();
            _cartService.DeleteFromCart(productId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

    }
}
