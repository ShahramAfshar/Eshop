using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.HomePages.Queries.GetSlider
{
    public interface IGetSliderService
    {
        ResultDto<List<GetSliderDto>> Execute();
    }

    public class GetSliderService : IGetSliderService
    {
        private readonly IDataBaseContext _context;

        public GetSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetSliderDto>> Execute()
        {
            var sliders = _context.Sliders.OrderByDescending(s=>s.Id).ToList()
                .Select(s=> new GetSliderDto() { 
                
                    Link=s.Link,
                    SourceImage=s.Src
                
                }).ToList();

            return new ResultDto<List<GetSliderDto>>() { 
            
                IsSuccess=true,
                Message="",
                Data=sliders
            
            };
        }
    }

    public class GetSliderDto
    {
        public string SourceImage { get; set; }
        public string Link { get; set; }
    }
}
