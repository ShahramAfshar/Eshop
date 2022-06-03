using EStore.Application.Services.HomePages.Queries.GetSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Models.ViewModel.HomePages
{
    public class HomePagesViewModel
    {
        public List<GetSliderDto> Sliders { get; set; }
    }
}
