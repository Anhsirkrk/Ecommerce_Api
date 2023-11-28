using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class LoginRepository : IloginRepository
    {
        private readonly EcommerceDailyPickContext _context;
        private readonly IUserRepository _userRepository;
        public LoginRepository(EcommerceDailyPickContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<LoginViewModel> GetUserByMobileNumber(LoginViewModel loginViewModel)
        
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Mobile == loginViewModel.Mobile);
            if (item == null)
            {
                var item2 = new UserViewModel
                {
                    UserTypeId = loginViewModel.UserTypeId,
                    IsActive=loginViewModel.IsActive,
                    Mobile = loginViewModel.Mobile,
                };

               var creatingnewuser = await _userRepository.CreateUser(item2);

                var user = new LoginViewModel
                {

                    UserFound = false,
                    UserId= creatingnewuser.UserId,
                    Firstname=creatingnewuser.Mobile,
                    Lastname=creatingnewuser.Mobile,
                    Mobile= creatingnewuser.Mobile,
                    Username=creatingnewuser.Mobile,
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
                    Mobile = item.Mobile,
                    UserTypeId = item.UserTypeId,

                };
                return user;

            }
        }

        public async Task<LoginViewModel> GetUserByEmail(LoginViewModel loginViewModel)
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginViewModel.Email );
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
                    Mobile = item.Mobile,
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
