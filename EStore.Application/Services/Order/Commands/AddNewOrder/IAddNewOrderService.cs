using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EStore.Domain.Entities.Orders;

namespace EStore.Application.Services.Order.Commands.AddNewOrder
{
    public interface IAddNewOrderService
    {
        ResultDto Execute(RequestAddNewOrderServiceDto request);
    }

    public class AddNewOrderService : IAddNewOrderService
    {
        private readonly IDataBaseContext _context;

        public AddNewOrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewOrderServiceDto request)
        {
            var user = _context.Users.Find(request.UserId);
            var requestPay = _context.RequestPays.Find(request.RequestPayId);
            var cart = _context.Carts.Include(c => c.CartItem).ThenInclude(c=>c.Product).Where(c => c.Id == request.CartId).FirstOrDefault();

            requestPay.IsPay = true;
            requestPay.PayDateTime = DateTime.Now;

            cart.Finished = true;

            EStore.Domain.Entities.Orders.Order order = new Domain.Entities.Orders.Order()
            {

                Address = "",
                OrderState = OrderState.Processing,
                RequestPay = requestPay,
                User = user

            };
            _context.Orders.Add(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (var item in cart.CartItem)
            {
                OrderDetail orderDetail = new OrderDetail() { 
                Order=order,
                Price= item.Product.Price,
                Product= item.Product,
                Count= item.Count
                
                };
                orderDetails.Add(orderDetail);
            }
            _context.OrderDetails.AddRange(orderDetails);

            _context.SaveChanges();

            return new ResultDto() {
            IsSuccess=true,
            Message=""
            };


        }
    }

    public class RequestAddNewOrderServiceDto
    {
        public int CartId { get; set; }
        public int RequestPayId { get; set; }
        public int UserId { get; set; }

    }
}
