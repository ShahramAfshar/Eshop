using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Users.Commands.RemoveUser
{
    public interface IRemoveUserService
    {
        ResultDto Execute(int userId);
    }
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDataBaseContext _contex;

        public RemoveUserService(IDataBaseContext context)
        {
            _contex = context;
        }
        public ResultDto Execute(int userId)
        {
            var user = _contex.Users.Find(userId);

            if (user == null)
            {
                return new ResultDto() { 
                IsSuccess=false,
                Message="کاربری با این مشخصات یافت نشد"
                };
            }
            else
            {
                user.RemoveTime = DateTime.Now;
                user.IsRemove = true;
                _contex.SaveChanges();
                return new ResultDto() { 
                IsSuccess=true,
                Message="عملیات حذف کاربر با موفقیت انجام شد"
                };
            }
        }
    }
}
