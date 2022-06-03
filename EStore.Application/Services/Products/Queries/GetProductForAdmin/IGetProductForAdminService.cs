using EStore.Common.Dto;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(int page = 1, int pageSize = 20);
    }

}
