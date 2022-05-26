using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;

namespace EStore.Application.Services.Users.Commands.EditUser
{
    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _context;
        public EditUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequsetEditUserDto requsetEditUser)
        {
            var user = _context.Users.Find(requsetEditUser.Id);
            if (user==null)
            {
                return new ResultDto() {
                    IsSuccess = false,
                    Message="کاربری یافت نشد"
                };
            }
            else
            {
                user.FullName = requsetEditUser.FullName;
                _context.SaveChanges();
                return new ResultDto() {
                    IsSuccess = true,
                    Message="ویرایش کاربر با موفقیت انجام شد"
                };
            }

        }
    }
}
