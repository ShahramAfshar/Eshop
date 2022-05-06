using System.Collections.Generic;


namespace EStore.Application.Services.Users.Commands.RegisterUser
{
    public class RequestRegisterUserDTO
    {
        public string FullName { get; set; }
        public string  Email { get; set; }
        public string  Password { get; set; }
        public string RePassword { get; set; }
        public List<RolesInRegisterUserDTO> Roles { get; set; }
    }
}
