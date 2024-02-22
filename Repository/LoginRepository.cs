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
        private DatabaseLogger _databaseLogger;
        public LoginRepository(EcommerceDailyPickContext context, IUserRepository userRepository, DatabaseLogger databaseLogger)
        {
            _context = context;
            _userRepository = userRepository;
            _databaseLogger = databaseLogger;

        }

        public async Task<LoginViewModel> GetUserByMobileNumber(LoginViewModel loginViewModel)
        {
            try
            {
                var item = await _context.Users.FirstOrDefaultAsync(x => x.Mobile == loginViewModel.Mobile);
                if (item == null && loginViewModel.CreateNewUserIfUserdoesntexist == true)
                {
                    var item2 = new UserViewModel
                    {
                        UserTypeId = loginViewModel.UserTypeId,
                        IsActive = loginViewModel.IsActive,
                        Mobile = loginViewModel.Mobile,
                    };

                    var creatingnewuser = await _userRepository.CreateUser(item2);
                    var cartdeatils = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == creatingnewuser.UserId);

                    var user = new LoginViewModel
                    {

                        UserFound = false,
                        UserId = creatingnewuser.UserId,
                        Firstname = creatingnewuser.Mobile,
                        Lastname = creatingnewuser.Mobile,
                        Mobile = creatingnewuser.Mobile,
                        Username = creatingnewuser.Mobile,
                        ResultMessage = creatingnewuser.ResultMessage,
                        IsNewUserCreated = true,
                        CartId= cartdeatils.CartId

                    };

                    return user;

                }
                else if (item == null)
                {
                    var user = new LoginViewModel
                    {
                        UserFound = false,
                        ResultMessage = "user not found"
                    };
                    return user;

                }
                else
                {
                    var cartdeatils = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == item.UserId);
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
                        CartId= cartdeatils.CartId

                    };
                   
                    return user;

                }
            }
            catch (Exception ex)
            {
                _databaseLogger?.SetUserId(loginViewModel.Mobile.ToString());
                _databaseLogger.LogError(ex.ToString(), "An Error Occured IN GetUserByMobileNumber");
                loginViewModel.ResultMessage = "An unexpectde error occcured";
                return loginViewModel;



            }

        }

        public async Task<LoginViewModel> GetUserByEmail(LoginViewModel loginViewModel)
        {
            try
            {
                var item = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginViewModel.Email);
                
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
                    var cartdeatils = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == item.UserId);
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
                        CartId= cartdeatils.CartId

                    };
                    return user;

                }
            }
            catch (Exception ex)
            {
                _databaseLogger?.SetUserId(loginViewModel.Email.ToString());
                _databaseLogger.LogError(ex.ToString(), "An Error Occured IN GetUserByEmail");
                loginViewModel.ResultMessage = "An unexpectde error occcured";
                return loginViewModel;
            }
        }


        public User CreateUser(User newuser)
        {
            try
            {
                _context.Users.Add(newuser);
                _context.SaveChanges();
                return newuser;
            }
            catch (Exception ex)
            {
                _databaseLogger?.SetUserId(newuser.Email.ToString());
                _databaseLogger.LogError(ex.ToString(), "An Error Occured IN CreateUser");
                // User = "An unexpectde error occcured";
                return newuser;

            }
        }
    }
}
