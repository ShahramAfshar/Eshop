using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EStore.Application.Services.Products.Queries.GetAllCategories
{
    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IDataBaseContext _context;

        public GetAllCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllCategoriesDto>> Execute()
        {
            var categories = _context.Categories
                .Include(c => c.ParentCategory)
                .Where(c => c.ParentCaregoryId != null)
                .ToList()
                .Select(c => new AllCategoriesDto()
                {
                    Id = c.Id,
                    Name = $"{c.ParentCategory.Name}-{c.Name}"

                }).ToList();

            return new ResultDto<List<AllCategoriesDto>>()
            {
                IsSuccess = true,
                Message = "دسته بندی های با موفقیت بارگزاری شد",
                Data = categories

            };
        }
    }
}
