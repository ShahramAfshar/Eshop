using EStore.Common.Dto;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Queries.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<CategoriesDto>> Execute(int? parentId);
    }
}
