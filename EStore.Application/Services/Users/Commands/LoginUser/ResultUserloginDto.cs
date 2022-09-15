using System.Collections.Generic;

namespace EStore.Application.Services.Users.Commands.LoginUser
{
    public class ResultUserloginDto
    {
        public long UserId { get; set; }
        public List<string> Roles { get; set; }
        public string Name { get; set; }
    }
}
