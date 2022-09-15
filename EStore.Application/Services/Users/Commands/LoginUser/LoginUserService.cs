using EStore.Application.Interfaces.Contexts;
using EStore.Common;
using EStore.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EStore.Application.Services.Users.Commands.LoginUser
{
    public class LoginUserService: ILoginUserService
    {
        private readonly IDataBaseContext _context;
        public LoginUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultUserloginDto> Execute(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new ResultDto<ResultUserloginDto>() { 
               
                 IsSuccess=false,
                Message="نام کابری یا کلمه عبور نمی تواند خالی باشد",
                 Data= new ResultUserloginDto()
                 {

                 }
                };
            }

            var user = _context.Users
                .Include(p => p.UserInRoles)
                .ThenInclude(p => p.Role)
                .Where(u => u.Email.Equals(email) && u.IsActive == true)
                .FirstOrDefault();


            if (user ==null)
            {
                return new ResultDto<ResultUserloginDto>()
                {

                    IsSuccess = false,
                    Message = "کاربری با ایمیل در سایت وجود ندارد",
                    Data = new ResultUserloginDto()
                    {

                    }
                };

            }

            var passwordHasher = new PasswordHasher();
            bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Password, password);

            if (resultVerifyPassword==false)
            {
                return new ResultDto<ResultUserloginDto>()
                {

                    IsSuccess = false,
                    Message = "رمز وارد شده اشتباه است",
                    Data = new ResultUserloginDto()
                    {

                    }
                };
            }

            List<string> roles =  new List<string>();
            foreach (var item in user.UserInRoles)
            {
                roles.Add(item.Role.Name);
            }

            return new ResultDto<ResultUserloginDto>()
            {

                IsSuccess = true,
                Message = "ورود به سایت با موفقیت انجام شد",
                Data = new ResultUserloginDto()
                {
                    Roles=roles,
                    Name=user.FullName,
                    UserId=user.Id
                }
            };

        }
    }
}
