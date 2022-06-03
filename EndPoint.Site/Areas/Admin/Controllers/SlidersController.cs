using EStore.Application.Services.HomePages.Commands.AddNewSlider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private IAddNewSliderSevice _addNewSliderSevice;

        public SlidersController(IAddNewSliderSevice addNewSliderSevice)
        {
            _addNewSliderSevice = addNewSliderSevice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file,string link)
        {
            _addNewSliderSevice.Execute(file, link);

            return View();
        }

    }
}
