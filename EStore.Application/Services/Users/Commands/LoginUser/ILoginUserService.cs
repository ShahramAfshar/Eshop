using EStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Users.Commands.LoginUser
{
    public interface ILoginUserService
    {
        ResultDto<ResultUserloginDto> Execute(string email, string password);
    }
}
