using EStore.Application.Interfaces.Contexts;
using EStore.Common;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EStore.Application.Services.Products.Queries.GetProductForAdmin
{
    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext _contex;

        public GetProductForAdminService(IDataBaseContext context)
        {
            _contex = context;
        }

        public ResultDto<ProductForAdminDto> Execute(int page = 1, int pageSize = 20)
        {

            int rowCount = 0;
            var products = _contex.Products
                .Include(p => p.Category)
                .ToPaged(page, pageSize, out rowCount)
                .Select(p => new ProductForAdminListDto()
                {
                    Id=p.Id,
                    Brand = p.Brand,
                    Description = p.Description,
                    Inventory = p.Inventory,
                    IsDisplay = p.IsDisplay,
                    Name = p.Name,
                    Price = p.Price,
                    Category=p.Category.Name
                }).ToList();

            return new ResultDto<ProductForAdminDto>()
            {
                IsSuccess = true,
                Message = "لیست محصولات با موفقیت بارگزاری شد",
                Data = new ProductForAdminDto()
                {
                    Products = products,
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = rowCount
                },

            };
        }
    }

}
