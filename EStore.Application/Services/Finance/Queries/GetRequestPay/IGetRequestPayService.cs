using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Finance.Queries.GetRequestPay
{
    public interface IGetRequestPayService
    {
        ResultDto<RequestPayDto> Execute(Guid guid);
    }

    public class GetRequestPayService : IGetRequestPayService
    {
        private readonly IDataBaseContext _contex;

        public GetRequestPayService(IDataBaseContext context)
        {
            _contex = context;
        }

        public ResultDto<RequestPayDto> Execute(Guid guid)
        {
            var requestPay = _contex.RequestPays.Where(p => p.Guid == guid).FirstOrDefault();

            if (requestPay != null)
            {
                return new ResultDto<RequestPayDto>()
                {
                    Data= new RequestPayDto() { Amount=requestPay.Amount,Id=requestPay.Id},
                    IsSuccess=true,
                    Message=""

                };
            }
            else
            {
                throw new Exception("request pay not found");
            }

        }
    }

    public class RequestPayDto
    {
        public int Amount { get; set; }
        public int Id { get; set; }
    }
}
