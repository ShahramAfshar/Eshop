using EStore.Application.Interfaces.Contexts;
using EStore.Common;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, int page, string searchKey, int pageSize, int? catId = null);
    }
    public class GetProductForSiteService : IGetProductForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetProductForSiteService(IDataBaseContext context)
        {
            this._context = context;
        }

        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, int page, string searchKey, int pageSize, int? catId = null)
        {
            int totalCount;

            var productsQuery = _context.Products
                 .Include(p => p.ProductImages).AsQueryable();


            if (catId != null)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == catId || p.Category.ParentCategoryId == catId).AsQueryable();
            }

            if (!string.IsNullOrEmpty(searchKey))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey)).AsQueryable();
            }

            switch (ordering)
            {
                case Ordering.NotOrder:
                    productsQuery = productsQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    productsQuery = productsQuery.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.BestSelling:

                    break;
                case Ordering.MostPopular:
                    break;
                case Ordering.TheNewest:
                    productsQuery = productsQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    productsQuery = productsQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.TheMostExpensive:
                    productsQuery = productsQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                default:
                    break;
            }



            var products = productsQuery.ToPaged(page, pageSize, out totalCount);

            Random random = new Random();
            return new ResultDto<ResultProductForSiteDto>()
            {

                IsSuccess = true,
                Message = "",
                Data = new ResultProductForSiteDto()
                {

                    TotalRow = totalCount,
                    Products = products.Select(p => new ProductForSiteDto()
                    {

                        Id = p.Id,
                        Star = random.Next(1, 5),
                        Title = p.Name,
                        ImageSource = p.ProductImages.FirstOrDefault().SourceImage,
                        Price = p.Price,
                        ViewCount=p.ViewCount

                    }).ToList(),
                },

            };
        }
    }

    public class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }

    }

    public class ProductForSiteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Star { get; set; }
        public string ImageSource { get; set; }
        public int Price { get; set; }
        public int ViewCount { get; set; }

    }

    public enum Ordering
    {
        NotOrder = 0,
        /// <summary>
        /// پربازدیدترین ها
        /// </summary>
        MostVisited = 1,
        /// <summary>
        /// پرفروش ترین ها
        /// </summary>
        BestSelling = 2,
        /// <summary>
        /// محبوب ترین ها
        /// </summary>
        MostPopular = 3,
        /// <summary>
        /// جدیدترین ها
        /// </summary>
        TheNewest = 4,
        /// <summary>
        /// ارزان ترین ها
        /// </summary>
        Cheapest = 5,
        /// <summary>
        /// گران ترین ها
        /// </summary>
        TheMostExpensive = 6,

    }
}
