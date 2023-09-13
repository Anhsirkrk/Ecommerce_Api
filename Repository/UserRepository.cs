using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly EcommercedemoContext context;
        public UserRepository(EcommercedemoContext _context)
        {
            context = _context;
        }
        public async Task<LoginViewModel> CreateUser(LoginViewModel loginViewModel)
        {
            try
            {
                if (context != null)
                {
                    if (loginViewModel != null)
                    {
                        var user = new User
                        {
                            Username = loginViewModel.Email,
                            UserTypeId = loginViewModel.UserTypeId,
                            Password = loginViewModel.Password,
                            Firstname = loginViewModel.Firstname,
                            Lastname = loginViewModel.Lastname,
                            Mobile = loginViewModel.Mobile,
                            Email = loginViewModel.Email,
                        };
                        var res = await context.Users.AddAsync(user);
                         await context.SaveChangesAsync();
                        loginViewModel.ResultMessage = " Created Succesfully ";
                        return loginViewModel;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {throw ex;}
        }


    }
}
