using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;

namespace EStore.Application.Services.Users.Commands.UserStatusChange
{
    public class UserStatusChangeService : IUserStatusChangeService
    {
        private readonly IDataBaseContext _context;

        public UserStatusChangeService(IDataBaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user==null)
            {
                return new ResultDto() { 
                IsSuccess=false,
                Message="کاربری با این مشخصات یافت نشد"
                };
            }
            else
            {
                user.IsActive = !user.IsActive;
                _context.SaveChanges();
                string userState = user.IsActive ? "فعال" : "غیر فعال";
                return new ResultDto() {
                IsSuccess=true,
                Message=$"کاربر با موفقیت {userState} شد."
                };
            }
        }
    }
}
