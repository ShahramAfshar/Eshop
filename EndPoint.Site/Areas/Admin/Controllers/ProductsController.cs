using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.Products.Commands.AddNewProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductFasad _productFasad;

        public ProductsController(IProductFasad productFasad)
        {
            _productFasad = productFasad;
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFasad.GetAllCategoriesService.Execute().Data,"Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequstAddNewProductDto requst,List<AddNewProductFeature> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            requst.Images = images;
            requst.Feature = features;

            return Json(_productFasad.AddNewProductService.Execute(requst));
        }

        public IActionResult Index(int page=1,int pageSize=20)
        {
            return View(_productFasad.GetProductForAdminService.Execute(page,pageSize).Data);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var product = _productFasad.GetProductDetailForAdminService.Execute(id).Data;
            return View(product);
        }
    }
}
