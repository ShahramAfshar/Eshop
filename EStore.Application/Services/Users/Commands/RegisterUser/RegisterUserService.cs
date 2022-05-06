using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Users;
using System.Collections.Generic;


namespace EStore.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;
        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRegisterUserDTO> Execute(RequestRegisterUserDTO request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultRegisterUserDTO>() 
                    {
                     Data=new ResultRegisterUserDTO()
                     {
                         UserId=0
                     },
                     IsSuccess=false,
                     Message="پست الکترونیکی را وارد کنید"

                    };

                }

                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDto<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "نام کاربری را وارد کنید"

                    };

                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "کلمه عبور را وارد کنید"

                    };

                }

                if (request.Password != request.RePassword)
                {
                    return new ResultDto<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "کلمه عبور و تکرار ان یکسان نیست"

                    };

                }

            


            User user = new User() { 
            Email=request.Email,
            FullName=request.FullName
            };

            List<UserInRole> userInRole = new List<UserInRole>();

            foreach (var item in request.Roles)
            {
                var role = _context.Roles.Find(item.Id);
                userInRole.Add(new UserInRole() { 
                Role=role,
                RoleId=item.Id,
                User=user,
                UserId=user.Id
                });
            }

            user.UserInRoles = userInRole;
            _context.Users.Add(user);
            _context.SaveChanges();

            return new ResultDto<ResultRegisterUserDTO>() { 
            Data= new ResultRegisterUserDTO() { UserId=user.Id},
            IsSuccess=true,
            Message="ثبت نام انجام شد"
            };


            }

            catch (System.Exception)
            {
                return new ResultDto<ResultRegisterUserDTO>()
                {
                    Data = new ResultRegisterUserDTO()
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد"

                };
            }


        }
    }
}
