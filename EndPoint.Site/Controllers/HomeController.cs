using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModel.HomePages;
using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.HomePages.Queries.GetHomePageImage;
using EStore.Application.Services.HomePages.Queries.GetSlider;
using EStore.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSliderService _getSliderService;
        private readonly IGetHomePageImageService _getHomePageImageService;
        private readonly IProductFasad _productFasad;


        public HomeController(ILogger<HomeController> logger, 
            IGetSliderService getSliderService,
            IGetHomePageImageService getHomePageImageService,
            IProductFasad productFasad)
        {
            _logger = logger;
            _getSliderService = getSliderService;
            _getHomePageImageService = getHomePageImageService;
            _productFasad = productFasad;
        }

        public IActionResult Index()
        {
            HomePagesViewModel homePagesViewModel = new HomePagesViewModel() {
                Sliders = _getSliderService.Execute().Data,
                HomePageImageDtos = _getHomePageImageService.Execute().Data,
                Camera = _productFasad.GetProductForSiteService.Execute(Ordering.TheNewest, 1, null, 6, 4).Data.Products,
            };
            return View(homePagesViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
