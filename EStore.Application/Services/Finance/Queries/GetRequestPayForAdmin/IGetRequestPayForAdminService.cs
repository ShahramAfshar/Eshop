using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Finance.Queries.GetRequestPayForAdmin
{
    public interface IGetRequestPayForAdminService
    {
        ResultDto<List<RequestPayDto>> Execute();
    }

    public class GetRequestPayForAdminService : IGetRequestPayForAdminService
    {
        private readonly IDataBaseContext _contex;

        public GetRequestPayForAdminService(IDataBaseContext context)
        {
            _contex = context;
        }

        public ResultDto<List<RequestPayDto>> Execute()
        {
            var requestPay = _contex.RequestPays.Include(p => p.User)
                .ToList()
                .Select(p => new RequestPayDto()
                {

                    Amount = p.Amount,
                    Authority = p.Authority,
                    Guid = p.Guid,
                    IsPay = p.IsPay,
                    PayDateTime = p.PayDateTime,
                    RefId = p.RefId,
                    UserName = p.User.FullName,
                    UserId = p.UserId,
                    RequestPayId=p.Id

                }).ToList();



            return new ResultDto<List<RequestPayDto>>()
            {
                Data = requestPay,
                IsSuccess = true,
                Message = ""

            };
        }
    }

    public class RequestPayDto
    {
        public int RequestPayId { get; set; }
        public Guid Guid { get; set; }

        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDateTime { get; set; }

        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}
