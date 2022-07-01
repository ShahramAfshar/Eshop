using EStore.Application.Services.HomePages.Commands.AddNewHomePageImage;
using EStore.Domain.Entities.HomePages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IAddNewHomePageImageService _addNewHomePageImageService;
        public HomePageImagesController(IAddNewHomePageImageService addNewHomePageImageService)
        {
            _addNewHomePageImageService = addNewHomePageImageService;
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link, ImageLocation imageLocation)
        {
            _addNewHomePageImageService.Execute(new AddNewHomePageImageDto()
            {
                fromFile = file,
                ImageLocation = imageLocation,
                Link = link

            });

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
