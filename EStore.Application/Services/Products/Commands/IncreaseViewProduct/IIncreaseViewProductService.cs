using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.Commands.IncreaseViewProduct
{
    public interface IIncreaseViewProductService
    {
        ResultDto Execute(int productId);
    }
    public class IncreaseViewProductService : IIncreaseViewProductService
    {
        private readonly IDataBaseContext _contex;

        public IncreaseViewProductService(IDataBaseContext contex)
        {
            _contex = contex;
        }

        public ResultDto Execute(int productId)
        {
          var product= _contex.Products.Find(productId);

            product.ViewCount++;
            _contex.SaveChanges();

            return new ResultDto() { 
            
                IsSuccess=true,
                Message="مقدار فیلد بازدید یک مقدار افزایش یافت"

            };
        }
    }

}
