using EStore.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.HomePages
{
   public class HomePageImages:BaseEntity
    {
        public string SourceImage { get; set; }
        public string  Link { get; set; }
        public  ImageLocation ImageLocation { get; set; }
    }

    public enum ImageLocation
    {
        LeftTopSlider=0,
        LeftBottomSlider=1,
        BottomSlider=2,
        centerFullScreen=3,
        GroupOne=4,
        GroupTwo=5,
    }
}
