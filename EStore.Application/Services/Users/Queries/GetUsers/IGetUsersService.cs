using EStore.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        List<GetUsersDTO> Execute(string searchKey, int page=1); 

    }

    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;

        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public List<GetUsersDTO> Execute(string searchKey, int page = 1)
        {
            var users = _context.Users.AsQueryable();
            if (! string.IsNullOrWhiteSpace(searchKey))
            {
                users = users.Where(u => u.FullName.Contains(searchKey) && u.Email.Contains(searchKey));
            }
        }
    }

    public class GetUsersDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
