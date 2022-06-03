using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFasad _productFasad;


        public ProductsController(IProductFasad productFasad)
        {
            _productFasad = productFasad;
        }
        [HttpGet]
        public IActionResult Index(Ordering ordering, string searchKey ,int? catId=null, int page=1,int pageSize=20)
        {
            var products = _productFasad.GetProductForSiteService.Execute(ordering, page,searchKey,pageSize,catId);
            return View(products.Data);
        }
        public IActionResult Detail(int id)
        {
            var product = _productFasad.GetProductDetailForSiteService.Execute(id).Data;
            return View(product);
        }
    }
}
