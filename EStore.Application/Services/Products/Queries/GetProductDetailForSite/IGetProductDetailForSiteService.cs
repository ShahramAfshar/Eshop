using EStore.Application.Interfaces.Contexts;
using EStore.Application.Services.Products.Commands.IncreaseViewProduct;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Queries.GetProductDetailForSite
{
    public interface IGetProductDetailForSiteService
    {

        ResultDto<ProductDetailForSiteDto> Execute(int productId);
    }

    public class GetProductDetailForSiteService : IGetProductDetailForSiteService
    {
        private readonly IDataBaseContext _context;
      //  private readonly IIncreaseViewProductService _increaseViewProductService;

        public GetProductDetailForSiteService(IDataBaseContext context)
        {
            _context = context;
          //  _increaseViewProductService = increaseViewProductService;
        }

        public ResultDto<ProductDetailForSiteDto> Execute(int productId)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p=>p.ProductImages)
                .Include(p=>p.ProductFeatures)
                .Where(p => p.Id == productId).SingleOrDefault();


            if (product==null)
            {
                return new ResultDto<ProductDetailForSiteDto>() { 
                
                    IsSuccess=false,
                    Message="محصول مورد نظر یافت نشد",
                    Data=null
                
                };
            }

            product.ViewCount++;
            _context.SaveChanges();

         //   _increaseViewProductService.Execute(product.Id);

            return new ResultDto<ProductDetailForSiteDto>() { 
            
                IsSuccess=true,
                Message="",
                Data= new ProductDetailForSiteDto() { 
                
                    Brand=product.Brand,
                    Id=product.Id,
                    Description=product.Description,
                    Price=product.Price,
                    Title=product.Name,
                    Category=$"{product.Category.ParentCategory.Name}-{product.Category.Name}",
                    ImageSource=product.ProductImages.Select(i=>i.SourceImage).ToList(),
                    Features=product.ProductFeatures.Select(f=> new ProductDetailForSiteFeatureDto() { 
                    
                        DisplayText=f.DisplayName,
                        Value=f.Value
                    
                    }).ToList()
                },
            
            };




            throw new NotImplementedException();
        }
    }


    public class ProductDetailForSiteDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string  Description { get; set; }
        public int Price { get; set; }

        public List<string> ImageSource { get; set; }
        public List<ProductDetailForSiteFeatureDto> Features { get; set; }
    }

    public class ProductDetailForSiteFeatureDto
    {
        public string  DisplayText { get; set; }
        public string Value { get; set; }

    }

}
