using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Order.Queries.GetOrdersForAdmin
{
    public interface IGetOrdersForAdminService
    {
        ResultDto<List<OrderDto>> Execute(OrderState orderState);
    }

    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetOrdersForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<OrderDto>> Execute(OrderState orderState)
        {
            var order = _context.Orders.Include(o => o.OrderDetails)
                .Where(o => o.OrderState == orderState)
                .OrderByDescending(o => o.Id)
                .ToList()
                .Select(o => new OrderDto()
                {
                    OrderState = o.OrderState,
                    ProductCount = o.OrderDetails.Count,
                    OrderId = o.Id,
                    UserId = o.UserId,
                    RequestPayId = o.RequestPayId,
                    InsertTime = o.InsertTime
                }).ToList();

            return new ResultDto<List<OrderDto>>()
            {
                Data = order,
                IsSuccess = true,
                Message = ""
            };
        }
    }


    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime InsertTime { get; set; }
        public int RequestPayId { get; set; }
        public int UserId { get; set; }
        public OrderState OrderState { get; set; }
        public int ProductCount { get; set; }
    }
}
