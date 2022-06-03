using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EStore.Application.Services.Products.Queries.GetCategories
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;

        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<List<CategoriesDto>> Execute(int? parentId)
        {

            var categories = _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategory)
                .Where(c => c.ParentCategoryId == parentId)
                .ToList()
                .Select(c => new CategoriesDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Parent = c.ParentCategory != null ? new ParentCategoryDto { Id = c.ParentCategory.Id, name = c.ParentCategory.Name } : null,
                    HasChild = c.SubCategory.Count() > 0 ? true : false,

                }).ToList();

            return new ResultDto<List<CategoriesDto>>() { 
            IsSuccess=true,
            Message="لیست با موفقیت برگشت داده شد",
            Data=categories,
            
            };

        }
    }
}
