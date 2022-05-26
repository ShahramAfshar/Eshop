using EStore.Application.Interfaces.FacadPatern;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFasad _productFasad;
        public CategoriesController(IProductFasad productFasad)
        {
            _productFasad = productFasad;
        }

        [HttpGet]
        public IActionResult Index(int? parentId)
        {
            return View(_productFasad.GetCategoriesService.Execute(parentId).Data);
        }

        [HttpGet]
        public IActionResult AddNewCategory(int? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(int? parentId, string name)
        {
            var result = _productFasad.AddNewCategorService.Execute(parentId, name);
            return Json(result);
        }
    }
}
