using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Order.Queries.GetUserOrders
{
    public interface IGetUserOrdersService
    {
        ResultDto<List<GetUserOrdersServiceDto>> Execute(int userId);
    }
    public class GetUserOrdersService : IGetUserOrdersService
    {
        private readonly IDataBaseContext _context;

        public GetUserOrdersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetUserOrdersServiceDto>> Execute(int userId)
        {

            var order = _context.Orders.Include(p => p.OrderDetails).ThenInclude(o => o.Product).Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id).ToList().Select(p => new GetUserOrdersServiceDto()
                {
                    OrderId = p.Id,
                    OrderState = p.OrderState,
                    RequestPayId = p.RequestPayId,
                    OrderDetails = p.OrderDetails.Select(o => new OrderDetailDto()
                    {
                        Count = o.Count,
                        OrderDerailId = o.Id,
                        price = o.Price,
                        ProductId = o.ProductId,
                        ProductName = o.Product.Name

                    }).ToList()
                }).ToList();

            return new ResultDto<List<GetUserOrdersServiceDto>>()
            {
                Data = order,
                IsSuccess = true,
                Message = ""

            };

        }
    }
    public class GetUserOrdersServiceDto
    {
        public int OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public int RequestPayId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
    public class OrderDetailDto
    {
        public int OrderDerailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int price { get; set; }
    }
}
