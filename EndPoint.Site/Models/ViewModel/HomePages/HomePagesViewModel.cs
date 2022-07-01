using EStore.Application.Services.HomePages.Queries.GetHomePageImage;
using EStore.Application.Services.HomePages.Queries.GetSlider;
using EStore.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Models.ViewModel.HomePages
{
    public class HomePagesViewModel
    {
        public List<GetSliderDto> Sliders { get; set; }
        public List<HomePageImageDto> HomePageImageDtos { get; set; }
        public List<ProductForSiteDto> Camera { get; set; }
        public List<ProductForSiteDto> Mobile { get; set; }
        public List<ProductForSiteDto> Laptop { get; set; }
        

    }
}
