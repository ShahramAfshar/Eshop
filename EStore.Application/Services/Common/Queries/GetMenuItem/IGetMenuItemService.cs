using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService
    {
        ResultDto<List<MenuItemDto>> Execute();
    }
    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext _context;

        public GetMenuItemService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<MenuItemDto>> Execute()
        {

            var category = _context.Categories
                .Include(c => c.SubCategory)
                .Where (c=>c.ParentCategoryId==null)
                .ToList()
                .Select(c => new MenuItemDto() { 
                
                    CatId=c.Id,
                    Name=c.Name,
                    child=c.SubCategory.Select(child=> new MenuItemDto() { 
                        CatId=child.Id,
                        Name=child.Name
                    
                    }).ToList(),
                
                }).ToList();

            return new ResultDto<List<MenuItemDto>>() { 
            
                IsSuccess=true,
                Message="با موفقیت بار گزاری شد",
                Data= category,
            
            };



        }
    }

    public class MenuItemDto
    {
        public int CatId { get; set; }
        public string Name { get; set; }
        public List<MenuItemDto> child { get; set; }
    }
}
