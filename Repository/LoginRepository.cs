using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class LoginRepository:ILoginRepository
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
                loginViewModel.UserFound = false;
                loginViewModel.ResultMessage = "User not found";
            }
            else
            {
                loginViewModel.UserFound = true;
                loginViewModel.UserId = item.UserId;
                loginViewModel.Username = item.Username;
                loginViewModel.Password = item.Password;
                loginViewModel.Email = item.Email;
                loginViewModel.Firstname = item.Firstname;
                loginViewModel.Lastname = item.Lastname;
                loginViewModel.Mobile = item.Mobile;
                loginViewModel.UserTypeId = item.UserId;
                return loginViewModel;
            }
            return loginViewModel;


        }
        public User CreateUser(User newuser)
        {
            _context.Users.Add(newuser);
            _context.SaveChanges();
            return newuser;
        }
    }
}
