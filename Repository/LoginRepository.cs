using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class LoginRepository : IloginRepository
    {
        private readonly EcommercedemoContext _context;
        public LoginRepository(EcommercedemoContext context)
        {
            _context = context;
        }

        public async Task<LoginViewModel> GetUserByMobileNumber(LoginViewModel loginViewModel)
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Mobile == loginViewModel.Mobile);
            if (item == null)
            {
                var user = new LoginViewModel
                {
                    UserFound = false,
                    ResultMessage = "User not found"
                };
                return user;

            }
            else
            {
                var user = new LoginViewModel
                {
                    UserFound = true,
                    UserId = item.UserId,
                    Username = item.Username,
                    Password = item.Password,
                    Email = item.Email,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    //Mobile = item.Mobile,
                    UserTypeId = item.UserTypeId,

                };
                return user;

            }
            }
        public User CreateUser(User newuser)
        {
            _context.Users.Add(newuser);
            _context.SaveChanges();
            return newuser;
        }
    }
}
