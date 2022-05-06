using System.Collections.Generic;

namespace EStore.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUsersDTO
    {
        public int Rows { get; set; }
        public List<GetUsersDTO> Users { get; set; }
    }
}
