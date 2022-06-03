using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Common.Queries.GetCategory
{
    public interface IGetCategoryService
    {
        ResultDto<List<GetCategoryDto>> Execute();
    }

    public class GetCategoryService : IGetCategoryService
    {
        private readonly IDataBaseContext _context;

        public GetCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetCategoryDto>> Execute()
        {
            var categories = _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();
               
            
            return new ResultDto<List<GetCategoryDto>>() { 
            
                IsSuccess=true,
                Message="",
                Data= categories.Select(c=> new GetCategoryDto() {               
                    CatId=c.Id,
                    CategoryName=c.Name
                }).ToList(),            
            };

            
        }
    }

    public class GetCategoryDto
    {
        public int CatId { get; set; }
        public string CategoryName { get; set; }
    }
}
