using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        List<GetUsersDTO> Execute(RequstGetUserDTO requstGetUserDTO); 

    }
}
