using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Finance.Commands.AddRequestPay
{
    public interface IAddRequestPayService
    {
        ResultDto<ResultRequestPayDto> Execute(int amount, int userId);
    }

    public class AddRequestPayService : IAddRequestPayService
    {
        private readonly IDataBaseContext _contex;

        public AddRequestPayService(IDataBaseContext context)
        {
            _contex = context;
        }

        public ResultDto<ResultRequestPayDto> Execute(int amount, int userId)
        {
            var user = _contex.Users.Find(userId);

            RequestPay requestPay = new RequestPay()
            {
                Amount = amount,
                Guid = Guid.NewGuid(),
                User = user,
                IsPay = false,
            };

            _contex.RequestPays.Add(requestPay);
            _contex.SaveChanges();

            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto() {
                    Guid = requestPay.Guid,
                    Email=user.Email,
                    Amount= requestPay.Amount,
                    RequestId= requestPay.Id
                },
                IsSuccess = true,
                Message = ""
            };

        }
    }

    public class ResultRequestPayDto
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public int RequestId { get; set; }
    }
}
