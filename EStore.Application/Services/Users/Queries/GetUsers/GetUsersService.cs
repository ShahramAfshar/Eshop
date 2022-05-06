using EStore.Application.Interfaces.Contexts;
using EStore.Common;
using System.Collections.Generic;
using System.Linq;

namespace EStore.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;

        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultGetUsersDTO Execute(RequstGetUserDTO requstGetUserDTO)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(requstGetUserDTO.SearchKey))
            {
                users = users.Where(u => u.FullName.Contains(requstGetUserDTO.SearchKey) && u.Email.Contains(requstGetUserDTO.SearchKey));
            }
            int rowCount = 0;
            var userList = users.ToPaged(requstGetUserDTO.Page, 20, out rowCount).Select(u => new GetUsersDTO()
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName
            }).ToList();

            return new ResultGetUsersDTO()
            {
                Users = userList,
                Rows = rowCount
            };
        }
    }
}
