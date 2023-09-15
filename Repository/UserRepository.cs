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
        public async Task<UserViewModel> CreateUser(UserViewModel userviewmodel)
        {
            try
            {
                if (context != null)
                {
                    if (userviewmodel != null)
                    {
                        var user = new User
                        {
                            Username = userviewmodel.Email,
                            UserTypeId = userviewmodel.UserTypeId,
                            Password = userviewmodel.Password,
                            Firstname = userviewmodel.Firstname,
                            Lastname = userviewmodel.Lastname,
                            Mobile = decimal.Parse(userviewmodel.Mobile),
                            Email = userviewmodel.Email,
                        };
                        var res = await context.Users.AddAsync(user);
                         await context.SaveChangesAsync();
                        userviewmodel.ResultMessage = " Created Succesfully ";
                        return userviewmodel;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {throw ex;}
        }
    }
}
