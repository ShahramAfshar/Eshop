using EStore.Common.Dto;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdminService
    {
        ResultDto<ProductDetailForAdminDto> Execute(int id);
    }

}
