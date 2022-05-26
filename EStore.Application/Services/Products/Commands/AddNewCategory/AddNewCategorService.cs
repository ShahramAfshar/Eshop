using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Products;

namespace EStore.Application.Services.Products.Commands.AddNewCategory
{
    public class AddNewCategorService : IAddNewCategoryService
    {
        private readonly IDataBaseContext _context;
        public AddNewCategorService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int? parentId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ResultDto()
                {
                    Message = "نام گروه نمی تواند خالی باشد",
                    IsSuccess = false,
                };
            }

            var category = new Category()
            {
                Name = name,
                ParentCategory= GetParentCategory(parentId),
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new ResultDto(){
            
            IsSuccess=true,
            Message="دسته بندی با موفقیت اضافه شد"
            };


        }

        private Category GetParentCategory(int? id)
        {
            return _context.Categories.Find(id);
        }
    }


}
