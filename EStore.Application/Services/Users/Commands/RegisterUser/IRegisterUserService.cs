using EStore.Common.Dto;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EStore.Application.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUserService
    {
        ResultDto<ResultRegisterUserDTO> Execute(RequestRegisterUserDTO request);

    }
}
