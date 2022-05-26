using EStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Commands.AddNewCategory
{
    public interface IAddNewCategoryService
    {
        ResultDto Execute(int? parentId, string name);
    }


}
