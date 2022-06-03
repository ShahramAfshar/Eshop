using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EStore.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public class GetProductDetailForAdminService : IGetProductDetailForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductDetailForAdminDto> Execute(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductImages)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            return new ResultDto<ProductDetailForAdminDto>()
            {
                IsSuccess = true,
                Message = "بارگزاری محصول با موفقیت انجام شد",
                Data = new ProductDetailForAdminDto()
                {
                    Brand = product.Brand,
                    Category = GetCategory(product.Category),
                    Description = product.Description,
                    Inventory = product.Inventory,
                    Name = product.Name,
                    IsDisplay = product.IsDisplay,
                    Id = product.Id,
                    Price = product.Price,
                    Feature = product.ProductFeatures.ToList().Select(p => new ProductDetailFeatureDto() { Id = p.Id, DisplayName = p.DisplayName, Value = p.Value }).ToList(),
                    Images = product.ProductImages.ToList().Select(p => new ProductDetailImageDto() { Id = p.Id, SourceImage = p.SourceImage }).ToList(),


                },


            };

        }

        private string GetCategory(Category category)
        {
            string result = category.ParentCategory != null ? $"{category.ParentCategory.Name}-" : "";
            return result += category.Name;
        }
    }

}
