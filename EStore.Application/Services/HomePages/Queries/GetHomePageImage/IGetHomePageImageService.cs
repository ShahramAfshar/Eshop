using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.HomePages.Queries.GetHomePageImage
{
    public interface IGetHomePageImageService
    {
        ResultDto<List<HomePageImageDto>> Execute();
    }
    public class GetHomePageImageService : IGetHomePageImageService
    {
        private readonly IDataBaseContext _context;

        public GetHomePageImageService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<HomePageImageDto>> Execute()
        {
            var result = _context.HomePageImages.OrderByDescending(h=>h.Id).ToList().Select(h => new HomePageImageDto()
            {

                Id = h.Id,
                ImageLocation = h.ImageLocation,
                Link = h.Link,
                SourceImage = h.SourceImage

            }).ToList();
            return new ResultDto<List<HomePageImageDto>>()
            {
                Data = result,
                IsSuccess = true,
                Message = "",

            };

        }
    }
    public class HomePageImageDto
    {
        public int Id { get; set; }
        public string SourceImage { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
