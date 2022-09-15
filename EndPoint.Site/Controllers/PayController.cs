using EStore.Application.Services.Carts;
using EStore.Application.Services.Finance.Commands.AddRequestPay;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using ZarinPal.Class;
using Dto.Payment;
using EStore.Application.Services.Finance.Queries.GetRequestPay;
using EStore.Application.Services.Order.Commands.AddNewOrder;

namespace EndPoint.Site.Controllers
{
    // [Authorize("Customer")] 
    public class PayController : Controller
    {
        private readonly IAddRequestPayService _addRequestPayService;
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IGetRequestPayService _getRequestPayService;
        private readonly IAddNewOrderService _addNewOrderService;


        public PayController(IAddRequestPayService addRequestPayService, ICartService cartService, IGetRequestPayService getRequestPayService, IAddNewOrderService addNewOrderService)
        {
            _addRequestPayService = addRequestPayService;
            _cartService = cartService;
            _cookiesManeger = new CookiesManeger();
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            _getRequestPayService = getRequestPayService;
            _addNewOrderService = addNewOrderService;
        }
        public async Task<IActionResult> Index()
        {
            int? userId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), userId);

            if (cart.Data.SumAmount > 0)
            {
                var requestPay = _addRequestPayService.Execute(cart.Data.SumAmount, userId.Value);



                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"http://localhost:3848/pay/Verify?guid={requestPay.Data.Guid}",
                    Description = "پرداخت فاکتور شماره " + requestPay.Data.RequestId,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");


            }
            else
            {
                return RedirectToAction("Index", "Carts");
            }
        }

        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var requestPay = _getRequestPayService.Execute(guid).Data;

            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestPay.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            int? userId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), userId);

            if (verification.Status == 100)
            {
                _addNewOrderService.Execute(new RequestAddNewOrderServiceDto()
                {

                    UserId = userId.Value,
                    RequestPayId = requestPay.Id,
                    CartId = cart.Data.CartId

                });

                //Redirect To Orders
                return RedirectToAction("Index", "Orders");
            }
            else
            {

            }


            return null;
        }
    }
}
